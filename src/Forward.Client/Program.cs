using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Forward.Client.Entities;
using Forward.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;

namespace Forward.Client
{
    public class Program
    {

        public static Dictionary<string, string> dictULR = new Dictionary<string, string> {
{"1","http://shop.99114.com"},
{"2","https://www.70dir.com"},
{"3","http://www.tianqishi.com"},
{"4","http://www.seek68.cn"},
{"5","http://zuopinj.com"},
{"6","http://www.52yxz.net"},
{"7","https://www.lxybaike.com"},
{"8","http://www.wangzhanmulu.net"},
{"9","http://www.wangluo379.cn"},
{"10","http://www.20dir.com"},
{"11","http://www.40dir.com"},
{"12","http://www.vvdir.com"},
{"13","http://www.hhdir.com"},
{"14","http://www.wangzhanmulu.net"},
{"15","http://www.8mtime.com"},
{"16","https://www.50dir.com"},
{"17","http://www.zhiyezhuangf.com"},
{"18","http://www.mhwz.cn"},
{"19","http://www.xianbaowu.cn"},
{"20","http://www.shuyi99.com"},
{"21","http://www.mfisp.com"},
{"22","http://www.ziyuanmaow.com"},
{"23","http://ip.tianqishi.com"},
{"24","https://www.chaxunle.cn"},
{"25","http://www.oxgood.com"},
{"26","http://www.huanlj.com"},
{"27","http://www.digg58.com"},
{"28","http://www.iapolo.com"},
{"29","http://www.at"},
{"30","https://www.wangzhanchi.com"},
{"31","https://www.wangzhanchi.com"},
{"32","https://www.wangzhanchi.com"},
{"33","https://www.wangzhanchi.com"},
{"34","https://www.wangzhanchi.com"},
{"35","https://www.wangzhanchi.com"},
{"36","https://hm.baidu.com"},
{"37","https://zz.bdstatic.com"},
{"38","http://push.zhanzhang.baidu.com"},
{"39","http://guowai.wangzhanchi.com"},
{"40","http://hk.wangzhanchi.com"},
{"41","http://www.beian.miit.gov.cn"},

        };

        static async Task Main(string[] args)
        {
            await runclient();
        }

        public static async Task runclient()
        {
            foreach (string u in dictULR.Values)
            {

                accessweb ab = new accessweb { WEBURL = u };
                ab.InitializeBackgroundWorker();
                ab.BGWorkerStart();

            }

            var _logger = LogManager.GetCurrentClassLogger();
            _logger.Warn("端口转发程序启动");

            try
            {
                using (var services = Configure())
                {
                    await services.GetService<ClientServer>().Start();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "程序出现异常退出");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
        /// <summary>
        /// 程序配置
        /// </summary>
        /// <returns></returns>
        private static ServiceProvider Configure()
        {
            var services = new ServiceCollection();
            var configuration = Configuration();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<ForwardService>();
            services.AddScoped<HeartbeatService>();
            services.AddSingleton<ClientServer>();
            services.AddTransient<TcpClient>();
            services.Configure<ForwardOptions>(configuration);
            services.AddLogging(options =>
            {
                options.AddNLog();
            });

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 构建配置文件
        /// </summary>
        /// <returns></returns>
        private static IConfiguration Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", false);

            return builder.Build();
        }
    }
}
