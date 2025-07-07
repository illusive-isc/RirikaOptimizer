using jp.illusive_isc.RirikaOptimizer;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(IllRirikaOptimizerDifinition))]

namespace jp.illusive_isc.RirikaOptimizer
{
    public class IllRirikaOptimizerDifinition : Plugin<IllRirikaOptimizerDifinition>
    {
        public override string QualifiedName => "IllusoryOverride.IllRirikaOptimizer";
        public override string DisplayName => "RirikaOptimizer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Resolving)
                .BeforePlugin("com.anatawa12.avatar-optimizer")
                .Run(IllRirikaOptimizerPass.Instance);
        }
    }
}
