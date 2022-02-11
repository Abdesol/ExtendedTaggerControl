# ExtendedTaggerControl

### Parts not working
**Tags binding from MainWindowViewModel to ExtendedTaggerControl is not working**
- The Tags property from the MainViewModel is bonded to the ExtendedTaggerControl TagsProperty. Although TagsProperty is two way binding; setting the Tags property initially
is not working. It shows that it is a new IEnumerable data with a length of "0".

#### **Tags binding from ExtendedTaggerControl to MainWindowViewModel is working**
