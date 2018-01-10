using System.Web;
using System.Web.Optimization;

namespace DeviceBase
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/ItDeviceList").Include(
                      "~/Scripts/ItDeviceList/itdevices.js"));
            bundles.Add(new ScriptBundle("~/bundles/PaDeviceList").Include(
                      "~/Scripts/PaDeviceList/padevices.js"));
            bundles.Add(new ScriptBundle("~/bundles/AsppDeviceList").Include(
                      "~/Scripts/AsppDeviceList/asppdevices.js"));

            bundles.Add(new ScriptBundle("~/bundles/ItDevice").Include(
                     "~/Scripts/ItDevice/device.js", "~/Scripts/jquery.datetimepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/AsppDevice").Include(
                    "~/Scripts/AsppDevice/device.js", "~/Scripts/jquery.datetimepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/PaDevice").Include(
                    "~/Scripts/PaDevice/device.js", "~/Scripts/jquery.datetimepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dialogs").Include(
                    "~/Scripts/jquery.modal.js", "~/Scripts/ItDevice/peoplemodal.js"));
            bundles.Add(new ScriptBundle("~/bundles/Export").Include("~/Scripts/jquery.modal.js", "~/Scripts/Export/export.modal.js"));
            bundles.Add(new ScriptBundle("~/bundles/DeviceClass").Include(
                   "~/Scripts/jquery.modal.js", "~/Scripts/Administration/DeviceClass/deviceclass.modal.js"));
            bundles.Add(new ScriptBundle("~/bundles/DeviceType").Include(
                   "~/Scripts/jquery.modal.js", "~/Scripts/Administration/DeviceType/devicetype.modal.js"));
            bundles.Add(new ScriptBundle("~/bundles/Location").Include(
                   "~/Scripts/jquery.modal.js", "~/Scripts/Administration/Location/location.modal.js"));
            bundles.Add(new ScriptBundle("~/bundles/Owner").Include(
                   "~/Scripts/jquery.modal.js", "~/Scripts/Administration/Owner/owner.modal.js"));
            bundles.Add(new ScriptBundle("~/bundles/ChangePassword").Include(
                  "~/Scripts/jquery.modal.js", "~/Scripts/Administration/ChangePassword/changepassword.modal.js"));
           
            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/MainStyle").Include("~/Content/MainStyle.css"));
            bundles.Add(new StyleBundle("~/Content/Export").Include("~/Content/Modal/Export.css", "~/Content/jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/DeleteConfirm").Include("~/Content/Modal/DeleteConfirm.css"));
            bundles.Add(new StyleBundle("~/Content/Administration").Include("~/Content/Administration.css"));

            bundles.Add(new StyleBundle("~/Content/LoginStyle").Include("~/Content/LoginStyle.css"));
            bundles.Add(new StyleBundle("~/Content/Manage").Include("~/Content/UserManage.css"));
            bundles.Add(new StyleBundle("~/Content/DeviceStyle").Include("~/Content/DeviceStyle.css", "~/Content/jquery.modal.css", "~/Content/jquery.datetimepicker.css"));
            bundles.Add(new StyleBundle("~/Content/Modal/DeviceClass").Include("~/Content/Modal/DeviceClass.css", "~/Content/jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/Modal/DeviceType").Include("~/Content/Modal/DeviceType.css", "~/Content/jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/Modal/Location").Include("~/Content/Modal/Location.css", "~/Content/jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/Modal/Owner").Include("~/Content/Modal/DeviceOwner.css", "~/Content/jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/Modal/ChangePassword").Include("~/Content/Modal/ChangePassword.css", "~/Content/jquery.modal.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}