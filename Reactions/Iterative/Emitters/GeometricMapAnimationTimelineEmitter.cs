using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using Core.Helpers.DependencyHelpers;
using Reactions.Iterative.Emitters.AnimationMap;

namespace Reactions.Iterative.Emitters
{
	[ContentProperty(nameof(AnimationMap))]
	public class GeometricMapAnimationTimelineEmitter : AnimationEmitterBase<double> 
	{
		public static readonly DependencyProperty AnimationMapProperty = DP.Register(
			new Meta<GeometricMapAnimationTimelineEmitter, LinearAnimationMap>());
		public LinearAnimationMap AnimationMap
		{
			get { return (LinearAnimationMap) GetValue(AnimationMapProperty); }
			set { SetValue(AnimationMapProperty, value); }
		}



		public override AnimationTimeline Emit(int index, int totalSteps, double? currentValue)
		{
			throw new System.NotImplementedException();
		}
	}
}
