using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace BackendWebService {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            // https://docs.microsoft.com/zh-tw/aspnet/core/security/cors
            services.AddCors (options => {
                options.AddPolicy ("AllowSpecificOrigins", policy => {
                    policy.WithOrigins ("http://0.0.0.0", "http://localhost");
                });
            });

            // https://docs.microsoft.com/zh-tw/aspnet/core/performance/response-compression
            services.AddResponseCompression (options => {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat (
                    new [] {
                        "image/png",
                        "image/svg+xml"
                    });
                options.Providers.Add<GzipCompressionProvider> ();
            });

            services.Configure<GzipCompressionProviderOptions> (options => {
                options.Level = CompressionLevel.Optimal;
            });

            // https://docs.microsoft.com/zh-tw/aspnet/core/tutorials/getting-started-with-swashbuckle
            services.AddSwaggerGen (c => {
                c.SwaggerDoc (
                    "v1",
                    new Info {
                        Title = "RESTful API",
                            Version = "1.0.0",
                            Description = "This is ASP.NET Core RESTful API Sample.",
                            TermsOfService = "None",
                            Contact = new Contact {
                                Name = "Johnny Chu",
                                    Url = "https://www.google.com"
                            },
                            License = new License {
                                Name = "Johnny Chu",
                                    Url = "https://cryptowatch.de/"
                            }
                    }
                );
            });

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "API V1");
            });
            app.UseResponseCompression ();
            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}