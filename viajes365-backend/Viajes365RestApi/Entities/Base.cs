using Microsoft.EntityFrameworkCore;
using System;

namespace Viajes365RestApi.Entities
{
    public class Base
    {
        [Comment("UserId del creador")]
        public long CreatorId { get; set; }

        [Comment("UserId del último Editor")]
        public long LastId { get; set; }

        [Comment("Fecha y hora de creación")]
        public DateTime Created { get; set; }

        [Comment("Fecha y hora de última actualización")]
        public DateTime Updated { get; set; }

        [Comment("Esto se implementa para soft delete")]
        public bool Active { get; set; }
    }
}
