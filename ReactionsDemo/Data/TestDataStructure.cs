using ReactionsDemo.ViewModels;

namespace ReactionsDemo.Data
{
	public class TestDataStructure : ViewModelBase
	{
		private int count;
		public int Count
		{
			get { return count; }
			set
			{
				count = value;
				NotifyOfPropertyChange(() => Count);
			}
		}

		private string description;
		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				NotifyOfPropertyChange(() => Description);
			}
		}
	}
}
