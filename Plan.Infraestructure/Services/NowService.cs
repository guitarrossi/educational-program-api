using Plan.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Infraestructure.Services
{
    public class NowService : INow
    {
        public DateTime Now => DateTime.Now;
    }
}
