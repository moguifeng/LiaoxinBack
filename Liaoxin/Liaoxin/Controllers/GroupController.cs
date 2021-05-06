using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Context;
using Zzb.Mvc;
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    public class GroupControllerController : BaseApiController
    {
        public IClientService clientService { get; set; }
        public LiaoxinContext Context { get; set; }

        public IGroupService groupService { get; set; }
        /// <summary>
        /// 获取客户
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetClient")]
        public ServiceResult<ClientBaseInfoResponse> GetClient()
        {
            var entity = clientService.GetClient();
            return (ServiceResult<ClientBaseInfoResponse>)Json(() =>
            {
                return ObjectGenericityResult<ClientBaseInfoResponse>(entity);
            }, "获取客户失败");

        }




    }


}