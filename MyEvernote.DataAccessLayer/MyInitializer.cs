using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //Admin Ekleme
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Coşkun",
                Surname = "Karakoç",
                UserName = "cskn",
                Password = "123456",
                Email = "coskunkarakocc@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                isActive = true,
                isAdmin = true,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "cskn",

            };
            //Standart Kullanıcı Ekleme
            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Ahmet Utku",
                Surname = "Ardıç",
                UserName = "aua",
                Password = "123456",
                Email = "ahmetutkuardıc@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                isActive = true,
                isAdmin = true,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "cskn",

            };
            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);
            //Örnek 8 Kullanıcı Ekleme
            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    UserName = $"user{i}",
                    Password = "123",
                    Email = FakeData.NetworkData.GetEmail(FakeData.NameData.GetFirstName(), FakeData.NameData.GetSurname()),
                    ActivateGuid = Guid.NewGuid(),
                    isActive = true,
                    isAdmin = false,
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}",

                };
                context.EvernoteUsers.Add(user);
            }
            context.SaveChanges();
            List<EvernoteUser> list = context.EvernoteUsers.ToList();
            //Fake Category Ekleme
            for (int i = 0; i < 10; i++)
            {
                var category = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "cskn"
                };
                context.Categories.Add(category);
                //Fake Note Ekleme
                for (int j = 0; j < FakeData.NumberData.GetNumber(min:5,max:9); j++)
                {
                    EvernoteUser owner = list[FakeData.NumberData.GetNumber(0, list.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = category,
                        isDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1,9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now ),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername =owner.UserName
                    };
                    category.Notes.Add(note);

                    //Fake Comment Ekleme
                    for (int k = 0; k < FakeData.NumberData.GetNumber(3,5); k++)
                    {
                        EvernoteUser owner_commment = list[FakeData.NumberData.GetNumber(0, list.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text =FakeData.TextData.GetSentence(),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername =owner_commment.UserName,
                            Owner = owner_commment


                        };
                        note.Comments.Add(comment);
                        
                    }

                    //Fake Like Ekleme
                 
                    for (int m = 0; m <note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = list[m]

                        };
                        note.Likes.Add(liked);
                    }


                }

               
            }

            context.SaveChanges();


        }
    }
}
