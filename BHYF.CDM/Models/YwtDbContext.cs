using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BHYF.CDM.Models
{
    public sealed class YwtDbContext : DbContext
    {
        public YwtDbContext()
            : base("YwtContext")
        {
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<YearDatum> YearDatum
        {
            get { return Set<YearDatum>(); }
        }
        public DbSet<DesignApply> DesignApply
        {
            get
            {
                return Set<DesignApply>();
            }
        }
        public DbSet<YedrThumb> YedrThumb
        {
            get { return Set<YedrThumb>(); }
        }

        public DbSet<DesignPlan> DesignPlan
        {
            get
            {
                return Set<DesignPlan>();
            }
        }

        public DbSet<PersonInfo> PersonInfo
        {
            get
            {
                return Set<PersonInfo>();
            }
        }

        public DbSet<Manager> Manager
        {
            get
            {
                return Set<Manager>();
            }
        }

        public DbSet<ApplyDesignManager> ApplyDesignManager
        {
            get
            {
                return Set<ApplyDesignManager>();
            }
        }

        public DbSet<DesignEvent> DesignEvent
        {
            get
            {
                return Set<DesignEvent>();
            }
        }

        public DbSet<YedrEvent> YedrEvent
        {
            get
            {
                return Set<YedrEvent>();
            }
        }

        public DbSet<ActivitySuperScholar> ActivitySuperScholar
        {
            get
            {
                return Set<ActivitySuperScholar>();
            }
        }

        #region 发货流程
        /// <summary>
        /// 发货申请
        /// </summary>
        public DbSet<DeliveryApply> DeliveryApply
        {
            get
            {
                return Set<DeliveryApply>();
            }
        }
        /// <summary>
        /// 发货商品信息
        /// </summary>
        public DbSet<DeliveryProduct> DeliveryProduct
        {
            get
            {
                return Set<DeliveryProduct>();
            }
        }
        /// <summary>
        /// 发货申请商品列表
        /// </summary>
        public DbSet<DeliveryApplyProduct> DeliveryApplyProduct
        {
            get
            {
                return Set<DeliveryApplyProduct>();
            }
        }

        /// <summary>
        /// 发货单
        /// </summary>
        public DbSet<DeliveryInvoice> DeliveryInvoice
        {
            get
            {
                return Set<DeliveryInvoice>();
            }
        }

        /// <summary>
        /// 收货回执单
        /// </summary>
        public DbSet<DeliveryReceipt> DeliveryReceipt
        {
            get
            {
                return Set<DeliveryReceipt>();
            }
        }

        public DbSet<DeliveryMenu> DeliveryMenu
        {
            get
            {
                return Set<DeliveryMenu>();
            }
        }

        public DbSet<DeliveryPower> DeliveryPower
        {
            get
            {
                return Set<DeliveryPower>();
            }
        }

        public DbSet<ManageGroup> ManageGroup
        {
            get
            {
                return Set<ManageGroup>();
            }
        }
        #endregion


        /// <summary>
        /// 指定单数形式的表名
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}