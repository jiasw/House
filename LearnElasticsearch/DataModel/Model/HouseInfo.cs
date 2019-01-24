﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace GetHouseInfo.DataBase.Model
{
    /// <summary>
    /// 实体类HouseInfo。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("HouseInfo")]
    [Serializable]
    public partial class HouseInfo : Entity
    {
        #region Model
        private int _ID;
        private string _Title;
        private string _Community;
        private string _HouseArea;
        private string _HouseDirection;
        private string _Ornamant;
        private string _Storey;
        private string _Location;
        private int? _WatchCount;
        private int? _LeadShowCount;
        private decimal? _PriceCount;
        private decimal? _Price;
        private string _Url;
        private int? _RoomCount;
        private int? _SaloonCount;
        private int? _BathRoomCount;
        private string _WramStyle;
        private string _PropertyTime;
        private string _Housestruct;
        private string _Building;
        private string _BuildingStruct;
        private string _Lift;
        private string _LiftScale;
        private string _HangOutTime;
        private string _LastSale;
        private string _HouseTime;
        private string _Mortgage;
        private string _Ownership;
        private string _HouseUseto;
        private string _Owne;
        private string _HouseLicence;
        private string _HouseInsideArea;
        private string _Area;

        /// <summary>
        /// 
        /// </summary>
        [Field("ID")]
        public int ID
        {
            get { return _ID; }
            set
            {
                this.OnPropertyValueChange("ID");
                this._ID = value;
            }
        }
        /// <summary>
        /// 房屋概览
        /// </summary>
        [Field("Title")]
        public string Title
        {
            get { return _Title; }
            set
            {
                this.OnPropertyValueChange("Title");
                this._Title = value;
            }
        }
        /// <summary>
        /// 社区名称
        /// </summary>
        [Field("Community")]
        public string Community
        {
            get { return _Community; }
            set
            {
                this.OnPropertyValueChange("Community");
                this._Community = value;
            }
        }
        /// <summary>
        /// 房屋建筑面积，单位：平米
        /// </summary>
        [Field("HouseArea")]
        public string HouseArea
        {
            get { return _HouseArea; }
            set
            {
                this.OnPropertyValueChange("HouseArea");
                this._HouseArea = value;
            }
        }
        /// <summary>
        /// 房屋方位：例如，南北通透
        /// </summary>
        [Field("HouseDirection")]
        public string HouseDirection
        {
            get { return _HouseDirection; }
            set
            {
                this.OnPropertyValueChange("HouseDirection");
                this._HouseDirection = value;
            }
        }
        /// <summary>
        /// 装修风格
        /// </summary>
        [Field("Ornamant")]
        public string Ornamant
        {
            get { return _Ornamant; }
            set
            {
                this.OnPropertyValueChange("Ornamant");
                this._Ornamant = value;
            }
        }
        /// <summary>
        /// 楼层信息
        /// </summary>
        [Field("Storey")]
        public string Storey
        {
            get { return _Storey; }
            set
            {
                this.OnPropertyValueChange("Storey");
                this._Storey = value;
            }
        }
        /// <summary>
        /// 地理位置
        /// </summary>
        [Field("Location")]
        public string Location
        {
            get { return _Location; }
            set
            {
                this.OnPropertyValueChange("Location");
                this._Location = value;
            }
        }
        /// <summary>
        /// 关注数量
        /// </summary>
        [Field("WatchCount")]
        public int? WatchCount
        {
            get { return _WatchCount; }
            set
            {
                this.OnPropertyValueChange("WatchCount");
                this._WatchCount = value;
            }
        }
        /// <summary>
        /// 带看数量
        /// </summary>
        [Field("LeadShowCount")]
        public int? LeadShowCount
        {
            get { return _LeadShowCount; }
            set
            {
                this.OnPropertyValueChange("LeadShowCount");
                this._LeadShowCount = value;
            }
        }
        /// <summary>
        /// 总价，单位：万元
        /// </summary>
        [Field("PriceCount")]
        public decimal? PriceCount
        {
            get { return _PriceCount; }
            set
            {
                this.OnPropertyValueChange("PriceCount");
                this._PriceCount = value;
            }
        }
        /// <summary>
        /// 每平方单价
        /// </summary>
        [Field("Price")]
        public decimal? Price
        {
            get { return _Price; }
            set
            {
                this.OnPropertyValueChange("Price");
                this._Price = value;
            }
        }
        /// <summary>
        /// 房屋信息url
        /// </summary>
        [Field("Url")]
        public string Url
        {
            get { return _Url; }
            set
            {
                this.OnPropertyValueChange("Url");
                this._Url = value;
            }
        }
        /// <summary>
        /// 卧室数量
        /// </summary>
        [Field("RoomCount")]
        public int? RoomCount
        {
            get { return _RoomCount; }
            set
            {
                this.OnPropertyValueChange("RoomCount");
                this._RoomCount = value;
            }
        }
        /// <summary>
        /// 客厅数量
        /// </summary>
        [Field("SaloonCount")]
        public int? SaloonCount
        {
            get { return _SaloonCount; }
            set
            {
                this.OnPropertyValueChange("SaloonCount");
                this._SaloonCount = value;
            }
        }
        /// <summary>
        /// 客厅数量
        /// </summary>
        [Field("BathRoomCount")]
        public int? BathRoomCount
        {
            get { return _BathRoomCount; }
            set
            {
                this.OnPropertyValueChange("BathRoomCount");
                this._BathRoomCount = value;
            }
        }
        /// <summary>
        /// 供暖方式
        /// </summary>
        [Field("WramStyle")]
        public string WramStyle
        {
            get { return _WramStyle; }
            set
            {
                this.OnPropertyValueChange("WramStyle");
                this._WramStyle = value;
            }
        }
        /// <summary>
        /// 产权时间
        /// </summary>
        [Field("PropertyTime")]
        public string PropertyTime
        {
            get { return _PropertyTime; }
            set
            {
                this.OnPropertyValueChange("PropertyTime");
                this._PropertyTime = value;
            }
        }
        /// <summary>
        /// 户型结构
        /// </summary>
        [Field("Housestruct")]
        public string Housestruct
        {
            get { return _Housestruct; }
            set
            {
                this.OnPropertyValueChange("Housestruct");
                this._Housestruct = value;
            }
        }
        /// <summary>
        /// 建筑类型
        /// </summary>
        [Field("Building")]
        public string Building
        {
            get { return _Building; }
            set
            {
                this.OnPropertyValueChange("Building");
                this._Building = value;
            }
        }
        /// <summary>
        /// 建筑结构
        /// </summary>
        [Field("BuildingStruct")]
        public string BuildingStruct
        {
            get { return _BuildingStruct; }
            set
            {
                this.OnPropertyValueChange("BuildingStruct");
                this._BuildingStruct = value;
            }
        }
        /// <summary>
        /// 配备电梯
        /// </summary>
        [Field("Lift")]
        public string Lift
        {
            get { return _Lift; }
            set
            {
                this.OnPropertyValueChange("Lift");
                this._Lift = value;
            }
        }
        /// <summary>
        /// 电梯比例
        /// </summary>
        [Field("LiftScale")]
        public string LiftScale
        {
            get { return _LiftScale; }
            set
            {
                this.OnPropertyValueChange("LiftScale");
                this._LiftScale = value;
            }
        }
        /// <summary>
        /// 挂牌时间
        /// </summary>
        [Field("HangOutTime")]
        public string HangOutTime
        {
            get { return _HangOutTime; }
            set
            {
                this.OnPropertyValueChange("HangOutTime");
                this._HangOutTime = value;
            }
        }
        /// <summary>
        /// 上次交易时间
        /// </summary>
        [Field("LastSale")]
        public string LastSale
        {
            get { return _LastSale; }
            set
            {
                this.OnPropertyValueChange("LastSale");
                this._LastSale = value;
            }
        }
        /// <summary>
        /// 房屋年限
        /// </summary>
        [Field("HouseTime")]
        public string HouseTime
        {
            get { return _HouseTime; }
            set
            {
                this.OnPropertyValueChange("HouseTime");
                this._HouseTime = value;
            }
        }
        /// <summary>
        /// 抵押信息
        /// </summary>
        [Field("Mortgage")]
        public string Mortgage
        {
            get { return _Mortgage; }
            set
            {
                this.OnPropertyValueChange("Mortgage");
                this._Mortgage = value;
            }
        }
        /// <summary>
        /// 交易权属
        /// </summary>
        [Field("Ownership")]
        public string Ownership
        {
            get { return _Ownership; }
            set
            {
                this.OnPropertyValueChange("Ownership");
                this._Ownership = value;
            }
        }
        /// <summary>
        /// 房屋用途
        /// </summary>
        [Field("HouseUseto")]
        public string HouseUseto
        {
            get { return _HouseUseto; }
            set
            {
                this.OnPropertyValueChange("HouseUseto");
                this._HouseUseto = value;
            }
        }
        /// <summary>
        /// 产权所属
        /// </summary>
        [Field("Owne")]
        public string Owne
        {
            get { return _Owne; }
            set
            {
                this.OnPropertyValueChange("Owne");
                this._Owne = value;
            }
        }
        /// <summary>
        /// 房本信息
        /// </summary>
        [Field("HouseLicence")]
        public string HouseLicence
        {
            get { return _HouseLicence; }
            set
            {
                this.OnPropertyValueChange("HouseLicence");
                this._HouseLicence = value;
            }
        }
        /// <summary>
        /// 套内面积
        /// </summary>
        [Field("HouseInsideArea")]
        public string HouseInsideArea
        {
            get { return _HouseInsideArea; }
            set
            {
                this.OnPropertyValueChange("HouseInsideArea");
                this._HouseInsideArea = value;
            }
        }
        /// <summary>
        /// 区域
        /// </summary>
        [Field("Area")]
        public string Area
        {
            get { return _Area; }
            set
            {
                this.OnPropertyValueChange("Area");
                this._Area = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.ID,
			};
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.ID;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.ID,
				_.Title,
				_.Community,
				_.HouseArea,
				_.HouseDirection,
				_.Ornamant,
				_.Storey,
				_.Location,
				_.WatchCount,
				_.LeadShowCount,
				_.PriceCount,
				_.Price,
				_.Url,
				_.RoomCount,
				_.SaloonCount,
				_.BathRoomCount,
				_.WramStyle,
				_.PropertyTime,
				_.Housestruct,
				_.Building,
				_.BuildingStruct,
				_.Lift,
				_.LiftScale,
				_.HangOutTime,
				_.LastSale,
				_.HouseTime,
				_.Mortgage,
				_.Ownership,
				_.HouseUseto,
				_.Owne,
				_.HouseLicence,
				_.HouseInsideArea,
				_.Area,
			};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._ID,
				this._Title,
				this._Community,
				this._HouseArea,
				this._HouseDirection,
				this._Ornamant,
				this._Storey,
				this._Location,
				this._WatchCount,
				this._LeadShowCount,
				this._PriceCount,
				this._Price,
				this._Url,
				this._RoomCount,
				this._SaloonCount,
				this._BathRoomCount,
				this._WramStyle,
				this._PropertyTime,
				this._Housestruct,
				this._Building,
				this._BuildingStruct,
				this._Lift,
				this._LiftScale,
				this._HangOutTime,
				this._LastSale,
				this._HouseTime,
				this._Mortgage,
				this._Ownership,
				this._HouseUseto,
				this._Owne,
				this._HouseLicence,
				this._HouseInsideArea,
				this._Area,
			};
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "HouseInfo");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ID = new Field("ID", "HouseInfo", "");
            /// <summary>
            /// 房屋概览
            /// </summary>
            public readonly static Field Title = new Field("Title", "HouseInfo", "房屋概览");
            /// <summary>
            /// 社区名称
            /// </summary>
            public readonly static Field Community = new Field("Community", "HouseInfo", "社区名称");
            /// <summary>
            /// 房屋建筑面积，单位：平米
            /// </summary>
            public readonly static Field HouseArea = new Field("HouseArea", "HouseInfo", "房屋建筑面积，单位：平米");
            /// <summary>
            /// 房屋方位：例如，南北通透
            /// </summary>
            public readonly static Field HouseDirection = new Field("HouseDirection", "HouseInfo", "房屋方位：例如，南北通透");
            /// <summary>
            /// 装修风格
            /// </summary>
            public readonly static Field Ornamant = new Field("Ornamant", "HouseInfo", "装修风格");
            /// <summary>
            /// 楼层信息
            /// </summary>
            public readonly static Field Storey = new Field("Storey", "HouseInfo", "楼层信息");
            /// <summary>
            /// 地理位置
            /// </summary>
            public readonly static Field Location = new Field("Location", "HouseInfo", "地理位置");
            /// <summary>
            /// 关注数量
            /// </summary>
            public readonly static Field WatchCount = new Field("WatchCount", "HouseInfo", "关注数量");
            /// <summary>
            /// 带看数量
            /// </summary>
            public readonly static Field LeadShowCount = new Field("LeadShowCount", "HouseInfo", "带看数量");
            /// <summary>
            /// 总价，单位：万元
            /// </summary>
            public readonly static Field PriceCount = new Field("PriceCount", "HouseInfo", "总价，单位：万元");
            /// <summary>
            /// 每平方单价
            /// </summary>
            public readonly static Field Price = new Field("Price", "HouseInfo", "每平方单价");
            /// <summary>
            /// 房屋信息url
            /// </summary>
            public readonly static Field Url = new Field("Url", "HouseInfo", "房屋信息url");
            /// <summary>
            /// 卧室数量
            /// </summary>
            public readonly static Field RoomCount = new Field("RoomCount", "HouseInfo", "卧室数量");
            /// <summary>
            /// 客厅数量
            /// </summary>
            public readonly static Field SaloonCount = new Field("SaloonCount", "HouseInfo", "客厅数量");
            /// <summary>
            /// 客厅数量
            /// </summary>
            public readonly static Field BathRoomCount = new Field("BathRoomCount", "HouseInfo", "客厅数量");
            /// <summary>
            /// 供暖方式
            /// </summary>
            public readonly static Field WramStyle = new Field("WramStyle", "HouseInfo", "供暖方式");
            /// <summary>
            /// 产权时间
            /// </summary>
            public readonly static Field PropertyTime = new Field("PropertyTime", "HouseInfo", "产权时间");
            /// <summary>
            /// 户型结构
            /// </summary>
            public readonly static Field Housestruct = new Field("Housestruct", "HouseInfo", "户型结构");
            /// <summary>
            /// 建筑类型
            /// </summary>
            public readonly static Field Building = new Field("Building", "HouseInfo", "建筑类型");
            /// <summary>
            /// 建筑结构
            /// </summary>
            public readonly static Field BuildingStruct = new Field("BuildingStruct", "HouseInfo", "建筑结构");
            /// <summary>
            /// 配备电梯
            /// </summary>
            public readonly static Field Lift = new Field("Lift", "HouseInfo", "配备电梯");
            /// <summary>
            /// 电梯比例
            /// </summary>
            public readonly static Field LiftScale = new Field("LiftScale", "HouseInfo", "电梯比例");
            /// <summary>
            /// 挂牌时间
            /// </summary>
            public readonly static Field HangOutTime = new Field("HangOutTime", "HouseInfo", "挂牌时间");
            /// <summary>
            /// 上次交易时间
            /// </summary>
            public readonly static Field LastSale = new Field("LastSale", "HouseInfo", "上次交易时间");
            /// <summary>
            /// 房屋年限
            /// </summary>
            public readonly static Field HouseTime = new Field("HouseTime", "HouseInfo", "房屋年限");
            /// <summary>
            /// 抵押信息
            /// </summary>
            public readonly static Field Mortgage = new Field("Mortgage", "HouseInfo", "抵押信息");
            /// <summary>
            /// 交易权属
            /// </summary>
            public readonly static Field Ownership = new Field("Ownership", "HouseInfo", "交易权属");
            /// <summary>
            /// 房屋用途
            /// </summary>
            public readonly static Field HouseUseto = new Field("HouseUseto", "HouseInfo", "房屋用途");
            /// <summary>
            /// 产权所属
            /// </summary>
            public readonly static Field Owne = new Field("Owne", "HouseInfo", "产权所属");
            /// <summary>
            /// 房本信息
            /// </summary>
            public readonly static Field HouseLicence = new Field("HouseLicence", "HouseInfo", "房本信息");
            /// <summary>
            /// 套内面积
            /// </summary>
            public readonly static Field HouseInsideArea = new Field("HouseInsideArea", "HouseInfo", "套内面积");
            /// <summary>
            /// 区域
            /// </summary>
            public readonly static Field Area = new Field("Area", "HouseInfo", "区域");
        }
        #endregion
    }
}