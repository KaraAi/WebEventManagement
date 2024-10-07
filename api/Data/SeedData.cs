using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
  public static class SeedData
  {
    public static void SeedDatabase(IApplicationBuilder app)
    {
      ArgumentNullException.ThrowIfNull(app);

      ApiContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApiContext>();

      if (context.Database.GetPendingMigrations().Any())
      {
        context.Database.Migrate();
      }

      if (!context.Administrators.Any())
      {
        context.Administrators.Add(new Administrator()
        {
          UserName = "Administrator",
          PassWord = "E10ADC3949BA59ABBE56E057F20F883E",
          FullName = "Administrator",
          Phone = "0708613704",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now
        });

        context.SaveChanges();
      }

      List<Events> events = [
        new Events()
        {
          EventCode = "EV-2024/01",
          EventName = "Tham dự lễ Tốt Nghiệp",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
        },
        new Events()
        {
          EventCode = "EV-2024/02",
          EventName = "Tham dự Chuyên đề ngành CNTT & CNTT(UDPM)",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
        },
        new Events()
        {
          EventCode = "EV-2024/03",
          EventName = "Công bố kết quả thi tuyển sinh",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
        },
      ];

      List<User> users = [
        new User()
        {
          EventID = 1,
          UserCode = "GUEST-2024/02",
          FullName = "Nguyễn Thị Thanh Ngân",
          CCCD = "012931028123",
          Phone = "0362211202",
          Facility = "Quốc Bảo Software",
          Office = "Trợ lý",
          Email = "kara.nttn@gmail.com",
          IsCheck = false,
          Description = "Không có thông tin",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
        },
        new User()
        {
          EventID = 2,
          UserCode = "GUEST-2024/07",
          FullName = "Hồ Ngọc Đăng Khoa",
          CCCD = "012931028243",
          Phone = "0708613704",
          Facility = "Quốc Bảo Software",
          Office = "Dev",
          Email = "dangkhoaplatinum@gmail.com",
          IsCheck = true,
          Description = "Không có thông tin",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
        },
        new User()
        {
          EventID = 3,
          UserCode = "GUEST-2024/06",
          FullName = "Kara Ai",
          CCCD = "147852369",
          Phone = "789456123",
          Facility = "Quốc Bảo Software",
          Office = "Thư Ký",
          Email = "yusora.tn@gmail.com",
          IsCheck = false,
          Description = "Không có thông tin",
          UserCreated = "Administrator",
          UserUpdated = "Administrator",
          DateCreated = DateTime.Now,
          DateUpdated = DateTime.Now,
        },
      ];

      if (!context.Events.Any())
      {
        context.Events.AddRange(events);
        context.SaveChanges();
      }

      if (!context.Users.Any())
      {
        context.Users.AddRange(users);
        context.SaveChanges();
      }
    }
  }
}