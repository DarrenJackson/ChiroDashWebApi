using ChiroDash.Application.Doctors.Models;
using ChiroDash.Application.Scorecards.Models;
using ChiroDash.Application.Targets.Models;
using ChiroDash.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ChiroDash.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ChiroDash API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(
                config =>
                {

                    config.CreateMap<Doctor, DoctorDto>();
                    config.CreateMap<Target, TargetDto>();
                    config.CreateMap<Scorecard, ScorecardDto>();
                    config.CreateMap<ScorecardToCreateDto, Scorecard>();
                    config.CreateMap<ScorecardToUpdateDto, Scorecard>();
                    config.CreateMap<DoctorToCreateDto, Doctor>();
                    config.CreateMap<DoctorToUpdateDto, Doctor>();
                    config.CreateMap<TargetToCreateDto, Target>();
                    config.CreateMap<TargetToUpdateDto, Target>();

                    config.CreateMap<Employee, DoctorDto>();
                    config.CreateMap<DoctorToCreateDto, Employee>();
                });

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChiroDash API V1");
            });

            app.UseMvc();
        }
    }
}
