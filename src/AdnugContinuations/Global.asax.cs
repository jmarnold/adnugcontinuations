using System;
using FubuMVC.Ajax;
using FubuMVC.Core;
using FubuMVC.Spark;
using FubuMVC.StructureMap;
using FubuMVC.Validation;
using FubuValidation.StructureMap;
using StructureMap.Configuration.DSL;

namespace AdnugContinuations
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FubuApplication
                .For<AdnugContinuationsFubuRegistry>()
                .StructureMapObjectFactory(x => x.AddRegistry<AdnugContinuationsRegistry>())
                .Bootstrap();
        }
    }

    public class AdnugContinuationsRegistry : Registry
    {
        public AdnugContinuationsRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                     });

            ForSingletonOf<IEntityRepository>().Use<EntityRepository>();
            this.FubuValidation();
        }
    }

    public class AdnugContinuationsFubuRegistry : FubuRegistry
    {
        public AdnugContinuationsFubuRegistry()
        {
            IncludeDiagnostics(true);

            Actions
                .IncludeClassesSuffixedWithController();

            Routes
                .IgnoreControllerNamesEntirely()
                .HomeIs<ProductController>(c => c.Index(null));

            Views
                .TryToAttachWithDefaultConventions();

            Import<RequestCorrelation>();
            Import<ValidationPreview>();

            this.UseSpark();
            this.Validation(x => x.Actions.Include(call => call.IsHttpPost() && !call.IsValidationPreview()));
        }
    }
}