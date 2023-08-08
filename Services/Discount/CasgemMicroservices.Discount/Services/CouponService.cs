using CasgemMicroservices.Discount.Dtos;
using CasgemMicroservices.Shared.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace CasgemMicroservices.Discount.Services
{
    public class CouponService : ICouponService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public CouponService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        // private readonly string _connectionString = "Server=DESKTOP-PK98KLS;initial catalog=CouponDb;integrated security=true;";
        public async Task<Response<NoContent>> CreateCouponAsync(CreateCouponDto couponDto)
        {

            var values = await _dbConnection.ExecuteAsync("insert into Coupon (Rate,Code,CreatedTime) values (@Rate,@Code,@CreatedTime)", couponDto);
            if (values > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Ekleme Sırasında Hata Oluştu.", 500);
        }

        public async Task<Response<NoContent>> DeleteCouponAsync(int id)
        {
            var values = await _dbConnection.ExecuteAsync("delete from Coupon where CouponId=@CouponId",new {CouponID=id});
            return (values > 0) ?  Response<NoContent>.Success(204) :  Response<NoContent>.Fail("Silme sırasında hata oluştu.", 500);
        }

        public async Task<Response<ResultCouponDto>> GetCouponByIdAsync(int id)
        {
            var values = (await _dbConnection.QueryAsync<ResultCouponDto>("select * from Coupon where CouponId=@CouponID")).FirstOrDefault();
            var parameters = new DynamicParameters();
            parameters.Add("@CouponId", id);
            return (values != null) ? Response<ResultCouponDto>.Success(values, 204) : Response<ResultCouponDto>.Fail("Kupon Bulunamadı", 500);
        }

        public async Task<Response<List<ResultCouponDto>>> GetCouponListAsync()
        {

            var values = await _dbConnection.QueryAsync<ResultCouponDto>("select * from Coupon");
            return Response<List<ResultCouponDto>>.Success(values.ToList(), 200);
        }

        public async Task<Response<NoContent>> UpdateCouponAsync(UpdateCouponDto couponDto)
        {
            var values = await _dbConnection.ExecuteAsync("update Coupon set Code=@Code,Rate=@Rate where CouponId=@CouponId");
            var parameters = new DynamicParameters();
            parameters.Add("@Rate", couponDto.Rate);
            parameters.Add("@Code", couponDto.Code);
            parameters.Add("@CouponId", couponDto.CouponId);

            return (values > 0) ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Kupon Bulunamadı",500);
        }
    }
}
