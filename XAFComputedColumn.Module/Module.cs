using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace XAFComputedColumn.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class XAFComputedColumnModule : ModuleBase
{
    public XAFComputedColumnModule()
    {
        // 
        // XAFComputedColumnModule
        // 
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifference));
        AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.CloneObject.CloneObjectModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
    {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }

    public override void Setup(XafApplication application)
    {
        base.Setup(application);
        // Manage various aspects of the application UI and behavior at the module level.

        // HACK: Enable XPO Logs
        EnableXpoDebugLog();
    }

    public override void CustomizeTypesInfo(ITypesInfo typesInfo)
    {
        base.CustomizeTypesInfo(typesInfo);
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);

        // Hide OID all Windows
        typesInfo.FindTypeInfo(typeof(XPObject)).KeyMember.AddAttribute(new BrowsableAttribute(false));
    }


    /// <summary>
    /// XPO Logs
    /// </summary>
    private static void EnableXpoDebugLog()
    {
        FieldInfo xpoSwitchF = typeof(ConnectionProviderSql).GetField(
            "xpoSwitch",
            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        TraceSwitch xpoSwitch = (TraceSwitch)xpoSwitchF.GetValue(null);
        xpoSwitch.Level = TraceLevel.Info;
    }
}
