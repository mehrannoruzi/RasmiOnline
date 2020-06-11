namespace RasmiOnline.Console
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
             BundleTable.EnableOptimizations = false;
            #region Styles
            var mainCss = new string[] {
                "~" + Links.Content.Styles.Shared.bootstrap_css,
                "~" + Links.Content.Styles.Shared.fontawesome_all_min_css,
                "~" + Links.Content.Styles.Shared.PersianCalendar_css,
                  "~" + Links.Content.Styles.Shared.Zebra_css,
                "~" + Links.Content.Styles.Layout.portal_css,
            };
            bundles.Add(new StyleBundleOrderer(Links.Bundles.Styles.DashboardStyles, new CssMinify()).Include(new string[] {
                "~" + Links.Content.Styles.Shared.material_design_iconic_font_min_css,
                "~" + Links.Content.Styles.Shared.material_bootstrap_wizard_css,
                "~" + Links.Content.Styles.Shared.sweetalert_css,
                "~" + Links.Content.Styles.Shared.animate_css,
                "~" + Links.Content.Styles.Shared.PersianCalendar_css,
                  "~" + Links.Content.Styles.Shared.Zebra_css,
                "~" + Links.Content.Styles.Layout.app_min_css,
                "~" + Links.Content.Styles.Shared.jquery_scrollbar_css,
                "~" + Links.Content.Styles.Console.common_css,
                "~" + Links.Content.Styles.Shared.select2_min_css
            }));

            bundles.Add(new StyleBundleOrderer(Links.Bundles.Styles.OAuthStyles, new CssMinify()).Include(mainCss.Union(new string[] {
                "~" + Links.Content.Styles.Console.OAuth.common_css
            }).ToArray()));

            bundles.Add(new StyleBundleOrderer(Links.Bundles.Styles.OrderLayout, new CssMinify()).Include(mainCss.Union(new string[] {
                "~" + Links.Content.Styles.Shared.material_design_iconic_font_min_css,
                "~" + Links.Content.Styles.Console.common_css,
                "~" + Links.Content.Styles.Console.Order.layout_css,
                "~" + Links.Content.Styles.Console.Order.common_css,
                "~" + Links.Content.Styles.Console.OrderItems.common_css,
                  "~" + Links.Content.Styles.Layout.app_min_css,
                "~" + Links.Content.Styles.Shared.material_bootstrap_wizard_css,
                "~" + Links.Content.Styles.Shared.sweetalert_css,
                "~" + Links.Content.Styles.Shared.animate_css,
                "~" + Links.Content.Styles.Shared.flickity_min_css
            }).ToArray()));
            #endregion

            #region Scripts
            var mainScripts = new string[] {
                "~" + Links.Scripts.jquery_1_10_2_min_js,
                "~" + Links.Scripts.tether_min_js,
                "~" + Links.Scripts.bootstrap_min_js,
                "~" + Links.Scripts.jquery_validate_js,
                "~" + Links.Scripts.jquery_validate_unobtrusive_js,
                "~" + Links.Scripts.jquery_unobtrusive_ajax_js,
                "~" + Links.Scripts.Plugins.validator_js,
                "~" + Links.Scripts.Plugins.bootstrap_notify_min_js,
                "~" + Links.Scripts.Plugins.FarsiType.jquery_farsiInput_js,
                "~" + Links.Scripts.Plugins.PersianCalendar.PersianCalendar_js,
                 "~" + Links.Scripts.Plugins.Zebra.zebra_datepicker_min_js,
                "~" + Links.Scripts.Console.common_js,
            };
            bundles.Add(new ScriptBundleOrderer(Links.Bundles.Scripts.MainScripts, new JsMinify()).Include(mainScripts));
            bundles.Add(new ScriptBundleOrderer(Links.Bundles.Scripts.DashboardScripts).Include(mainScripts.Union(new string[] {
                "~" + Links.Scripts.waves_min_js,
                "~" + Links.Scripts.jquery_scrollbar_js,
                "~" + Links.Scripts.jquery_scrollLock_min_js,
                "~" + Links.Scripts.Plugins.SweetAlert.sweetalert_min_js,
                "~" + Links.Scripts.app_min_js,
                "~" + Links.Scripts.Plugins.jquery_mask_js,
                "~" + Links.Scripts.Plugins.select2_full_js
            }).ToArray()));

            bundles.Add(new ScriptBundleOrderer(Links.Bundles.Scripts.OAuthScripts, new JsMinify()).Include(mainScripts.Union(new string[] {
                "~" + Links.Scripts.Console.OAuth.index_js,
                "~" + Links.Scripts.Plugins.FarsiType.jquery_farsiInput_js
            }).ToArray()));

            bundles.Add(new ScriptBundleOrderer(Links.Bundles.Scripts.OrderLayout, new JsMinify()).Include(mainScripts.Union(new string[] {
                "~" + Links.Scripts.Console.Order.common_js,
                "~" + Links.Scripts.Console.order_item_js,
                "~" + Links.Scripts.Plugins.Flickity.flickity_pkgd_min_js,
            }).ToArray()));
            #endregion
        }
    }
}
