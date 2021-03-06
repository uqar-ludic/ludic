﻿using System.Web;
using System.Web.Optimization;

namespace SiteSandBoxDemo
{
    public class BundleConfig
    {
        // Pour plus d'informations sur Bundling, accédez à l'adresse http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Date").Include(
                        "~/Scripts/_Date.js"));

            bundles.Add(new ScriptBundle("~/bundles/Console").Include(
                        "~/Scripts/_Console.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                        "~/Scripts/ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/Ace").Include(
                        "~/Scripts/ace-builds-master/src-min-noconflict/ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/Ace-t").Include(
                        "~/Scripts/ace-builds-master/src-min-noconflict/theme-twilight.js"));

            bundles.Add(new ScriptBundle("~/bundles/Ace-cs").Include(
                        "~/Scripts/ace-builds-master/src-min-noconflict/mode-csharp.js"));

            bundles.Add(new ScriptBundle("~/bundles/Prompt").Include(
                        "~/Scripts/_Prompt.js"));

            bundles.Add(new ScriptBundle("~/bundles/JAce").Include(
                        "~/Scripts/jquery-ace.min.js"));

            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l'outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css",
                                                                "~/Content/Prompt.css"));

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
            BundleTable.EnableOptimizations = true;
        }
    }
}