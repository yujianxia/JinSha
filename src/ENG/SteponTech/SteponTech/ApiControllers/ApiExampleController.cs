using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SteponTech.ApiControllers
{
    /*
     WebApi说明，WebApi的Controller已经和普通Controller合并了，可以通过如下的属性路由来区别，同时，如果采用Post传递对象，需要标注FromBody（如下）。
     另外，需要注意的时，在返回对象时，命名会自动转换为骆驼命名法，但是一般键值对（以字符串作为键）的对象按照原始数据返回，例如JObject，Dictionary<>,JArray等
     正式开发时，请删除此类
     */

    /*
     关于Swagger的说明：
     Swagger是用于Web Api文档自动生成的工具，此框架中已经默认加入，便于前后端开发时的交流，仅在开发模式下启用，如果部署到生成环境中，则Nothing法访问，请注意。
     Swagger自动生成API的要求为控制器上添加了Route特性并指定了路径，方法里面包含Http谓词特性（或仅谓词中指定了路径），则会被纳入Swagger自动生成中。
     请注意，所有Web Api的类和方法请完善注释，注释将自动生成到文档中。
     大家可以在没有删除此类的情况下，运行此框架，查看文档效果。
     默认文档访问地址：http://your_site/swagger/ui
     */

    /// <summary>
    /// Web Api示例
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class ApiExampleController : Controller
    {
        // GET: api/ApiExample
        /// <summary>
        /// Get 操作示例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/ApiExample/5
        /// <summary>
        /// 带参数的Get操作示例
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // GET api/ApiExample/MultiGet
        /// <summary>
        /// 自定义属性路由的Get操作示例
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public string MultiGet()
        {
            //注意，如果出现冲突，例如此方法与上面的带参数Get路由冲突，则以这个优先级最高
            return "MultiGet";
        }

        // POST api/ApiExample
        /// <summary>
        /// Post操作示例
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST api/ApiExample/MultiPost
        /// <summary>
        /// 自定义路由的POST操作示例
        /// </summary>
        /// <param name="value">提交的参数</param>
        /// <returns>数据处理结果</returns>
        [HttpPost("[action]")]
        public dynamic MultiPost([FromBody] PostParameter value)
        {
            //注意这里返回的数据将是{"data":{"name":"Sweet","age":30,"joinDate":"2016-10-13T17:58:19.1693044+08:00"},"state":true}
            //所有的键被转换为骆驼命名
            return new {Data = new {Name = "Sweet", Age = 30, JoinDate = DateTime.Now}, State = true};
        }

        // PUT api/ApiExample/5
        /// <summary>
        /// PUT操作示例
        /// </summary>
        /// <param name="id">数据Id</param>
        /// <param name="value">值</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/ApiExample/5
        /// <summary>
        /// 数据DELETE示例
        /// </summary>
        /// <param name="id">数据ID</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    /// <summary>
    /// POST示例参数
    /// </summary>
    public class PostParameter
    {
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 处理事件
        /// </summary>
        public DateTime Time { get; set; }
    }
}