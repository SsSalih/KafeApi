using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.OrderDtos
{
    public static class OrderStatus
    {
        public const string Hazırlanıyor = "HAZIRLANIYOR:";
        public const string Hazir = "HAZIR:";
        public const string TeslimEdildi = "TESLIM_EDILDI:";
        public const string IptalEdildi = "IPRAL_EDILDI:";
        public const string Odendi = "SİPARİS_ODENDI:";
    }
}









