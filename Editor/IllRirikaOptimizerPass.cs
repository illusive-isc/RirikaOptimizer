using nadena.dev.ndmf;
using UnityEngine;

namespace jp.illusive_isc.RirikaOptimizer
{
    public class IllRirikaOptimizerPass : Pass<IllRirikaOptimizerPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (
                IllRirikaOptimizer IllRirikaOptimizer in context.AvatarRootObject.transform.GetComponents<IllRirikaOptimizer>()
            )
            {
                Object.DestroyImmediate(IllRirikaOptimizer.gameObject);
            }
        }
    }
}
