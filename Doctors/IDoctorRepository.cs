using System.Threading.Tasks;

namespace ChiroDashWebApi.Doctors
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetById(int id);
    }
}