using System.Security.Claims;

namespace API.Extensions
{
    /*
    Để tạo một cookie nắm giữ thông tin người dùng, bạn phải xây dựng một ClaimsPrincipal
    Nếu ko muốn người dùng gửi 1 username bất kì thì phải bắt người dùng gửi lại token đã cấp cho username
    Muốn người dùng gửi lại token mà mình đã cấp token cho username đấy thì dùng ClaimTypes
    */
    public static class ClaimsPrincipleExtensions
    {
        //13. Making the Last Active action filter more optimal
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}