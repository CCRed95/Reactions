using Caliburn.Micro;
using ReactionsDemo.Data;

namespace ReactionsDemo.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public BindableCollection<TestDataStructure> TestDataSource =>
			new BindableCollection<TestDataStructure>(new []
			{
				new TestDataStructure {Count = 53, Description = "Hello" },
				new TestDataStructure {Count = 23, Description = "Stuff" },
				new TestDataStructure {Count = 65, Description = "Some" },
				new TestDataStructure {Count = 32, Description = "More" },
				new TestDataStructure {Count = 58, Description = "Nonsense" },
				new TestDataStructure {Count = 90, Description = "For" },
				new TestDataStructure {Count = 35, Description = "Descriptions" },
				new TestDataStructure {Count = 63, Description = "And" },
				new TestDataStructure {Count = 12, Description = "All" },
				new TestDataStructure {Count = 28, Description = "That" },
				new TestDataStructure {Count = 77, Description = "Garbage" },
			}); 

		private TestDataStructure selectedData;
		public TestDataStructure SelectedData
		{
			get { return selectedData; }
			set
			{
				selectedData = value;
				NotifyOfPropertyChange(() => SelectedData);
			}
		}

		public MainViewModel()
		{
			
		}
		
	}
}
