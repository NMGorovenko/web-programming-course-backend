using Bogus;
using Microsoft.AspNetCore.Identity;
using Sfu.Shop.Domain.Entities;
using Sfu.Shop.Domain.IdentityEntities;
using Sfu.Shop.Infrastructure.DataAccess;

namespace Sfu.Shop.Web.Infrastructure.Seeders;

public class Seeder
{
    private readonly UserManager<User> userManager;
    private readonly AppDbContext dbContext;
    
    private readonly Faker faker = new("en");

    private readonly IList<User> Users;
    public Seeder(UserManager<User> userManager, AppDbContext dbContext)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;

        Users = userManager.Users.ToList();
    }

    public User GenerateUser()
    {
        var email = faker.Internet.Email(provider: "sfu.com");
        return new User
        {
            FirstName = faker.Name.FirstName(),
            LastName = faker.Name.LastName(),
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = faker.Phone.PhoneNumber("###-###-#####"),
            PhoneNumberConfirmed = true,
        };
    }

    public Product GenerateProduct()
    {
        var product = new Product()
        {
            ImageUrl = "https://bugulma.1sota.ru/images/no_photo.png",
            Title = "Food " + faker.Random.Int(0, 10000),
            Price = faker.Random.Int(10, 1000),
            Feedback = GenerateFeedback()
        };

        return product;
    }

    private IEnumerable<Feedback> GenerateFeedback()
    {
        var feedbacks = new List<Feedback>();
        var random = new Random();
        for (int i = 0; i < faker.Random.Int(0, 100); ++i)
        {
            var userNumber = random.Next(Users.Count());
            feedbacks.Add(new Feedback()
            {
                FeedbackUser = Users[userNumber],
                Text = faker.Lorem.Sentence(),
                Estimation = GenerateFeedbackScore(),
                CreatedAt = faker.Date.Past().ToUniversalTime()
            });
        }

        return feedbacks;
    }


    private int GenerateFeedbackScore()
    {
        var listScores = new List<int>()
        {
            1, 1, 1,
            2, 2, 2, 2, 2,
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
            4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
            5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5
        };
        var random = new Random();

        return listScores[random.Next(listScores.Count)];
    }
}
