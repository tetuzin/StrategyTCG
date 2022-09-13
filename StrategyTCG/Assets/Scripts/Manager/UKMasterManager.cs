using ShunLib.Manager.Master;

namespace UK.Manager.Master
{
    public class UKMasterManager : MasterManager
    {
        // Daoクラスの名前リスト（新しく作成したDaoクラスはここに記述する）
        protected static readonly string[] DAO_CLASS_NAME = {
            "CardMainDao",
            "CountryMainDao",
            "EffectMainDao",
            "EffectAbilityDao",
            "EffectGroupDao",
        };
        
        // Daoクラスの名前空間
        protected readonly string DAO_CLASS_NAMESPACE = "UK.Dao.";
        
        // DAOクラス名の配列を返す
        protected override string[] GetDaoClassNameList()
        {
            return DAO_CLASS_NAME;
        }
        
        // DAOクラスの名前空間を返す
        protected override string GetDaoClassNamespace()
        {
            return DAO_CLASS_NAMESPACE;
        }
    }
}