using CasgemMicroservices.Discount.Dtos;
using CasgemMicroservices.Shared.Dtos;

namespace CasgemMicroservices.Discount.Services
{
    public interface ICouponService
    {
        Task<Response<List<ResultCouponDto>>> GetCouponListAsync();
        Task<Response<ResultCouponDto>> GetCouponByIdAsync(int id);
        Task<Response<NoContent>> CreateCouponAsync(CreateCouponDto couponDto);
        Task<Response<NoContent>> UpdateCouponAsync(UpdateCouponDto couponDto);
        Task<Response<NoContent>> DeleteCouponAsync(int id);
    }
}
