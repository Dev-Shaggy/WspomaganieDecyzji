using PogromcaDylematów.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PogromcaDylematów
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoadListFiles fileList;
        private LoadSaveFile IOFile;

        private List<Alternative> Alternatives;
        private Criterias criterias;
        private List<Parameters> ParametersList;

        private EditCriteriaWindow editCriteriaWindow;
        private EditAlternativeWindow editAlternativeWindow;
        private NewFileWindow newFileWindow;
        private SelectFileWindow selectFileWindow;
        private ChartWindow chartWindow;

        private List<Alternative> AlternativeQuestions;
        private Criterias CriteriaQuestions;

        private Combinations combinationsCriteria, combinationAlternatives;

        private List<QuestionModel> QuestionsCriteria,QuestionsAlternative;

        private Weights weightsCriteria;
        private List<Weights> weightsAlternatives;

        private CriteriaValues cValues;
        private List<CriteriaValues> aValues;

        private List<KeyValuePair<string, double>> data;
        private List<int> index;
        private double errorLevel = 0.1;

        private bool alternativesSomplete, criteriasComplete;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            InitializeFileList();
            InitializeListBoxes();
        }
        private void Initialize()
        {
            IOFile = new LoadSaveFile();
            fileList = new LoadListFiles();
            Alternatives = new List<Alternative>();
            criterias = new Criterias();
            ParametersList = new List<Parameters>();

            combinationsCriteria = new Combinations();
            combinationAlternatives = new Combinations();
        }
        private void InitializeFileList()
        {
            fileList.LoadData();
            FileList.Items.Clear();
            for (int i = 0; i < fileList.Data.Count; i++)
            {
                FileList.Items.Add(fileList.Data[i]);
            }
            FileList.SelectionMode = SelectionMode.Single;
        }
        private void InitializeListBoxes()
        {
            CriteriasListBox.Items.Clear();
            AlternativesListBox.Items.Clear();

            for (int i = 0; i < criterias.CriteriasNames.Count; i++)
            {
                CriteriasListBox.Items.Add(criterias.CriteriasNames[i]);
            }
            CriteriasListBox.SelectionMode = SelectionMode.Single;

            for (int i = 0; i < Alternatives.Count; i++)
            {
                AlternativesListBox.Items.Add(Alternatives[i].Name);
            }
            AlternativesListBox.SelectionMode = SelectionMode.Single;
        }
        private void InitializeTreeView()
        {
            FileContentTreeView.Items.Clear();
            for (int i = 0; i < Alternatives.Count; i++)
            {

                TreeViewItem newItem = new TreeViewItem();
                newItem.Header = Alternatives[i].Name;
                for (int j = 0; j < Alternatives[i].parameters.parameters.Count; j++)
                {
                    newItem.Items.Add(new TreeViewItem() { Header = criterias.CriteriasNames[j] + "   " + Alternatives[i].parameters.parameters[j] });
                }
                FileContentTreeView.Items.Add(newItem);
            }
        }
        private void InitializeSelectBoxes(int id)
        {
            IOFile = new LoadSaveFile();
            IOFile.LoadData(fileList.Data[id]);

            CriteriaSwitch.Items.Clear();
            AlternativeSwitch.Items.Clear();

            Alternatives.Clear();
            criterias.Clear();
            Alternatives = IOFile.getAlternatives();
            criterias = IOFile.getCriterias();

            for (int i = 0; i < criterias.CriteriasNames.Count; i++)
            {
                CriteriaSwitch.Items.Add(criterias.CriteriasNames[i]);
            }
            CriteriaSwitch.SelectionMode = SelectionMode.Multiple;

            for (int i = 0; i < Alternatives.Count; i++)
            {
                AlternativeSwitch.Items.Add(Alternatives[i].Name);
            }
            AlternativeSwitch.SelectionMode = SelectionMode.Multiple;
        }

        private void InitializeCriteriaQuestions()
        {
            scrollQuestionCriterias.Children.Clear();
            QuestionsCriteria = new List<QuestionModel>();

            for (int i = 0; i < combinationsCriteria.CombinationList.Count; i++)
            {
                scrollQuestionCriterias.RowDefinitions.Add(new RowDefinition());
                string A = CriteriaQuestions.CriteriasNames[combinationsCriteria.CombinationList[i].itemOne];
                string B = CriteriaQuestions.CriteriasNames[combinationsCriteria.CombinationList[i].itemTwo];
                QuestionsCriteria.Add(new QuestionModel(A,B,0));
                Grid.SetColumn(QuestionsCriteria[i].OptionA, 0);
                Grid.SetColumn(QuestionsCriteria[i].OptionB, 2);
                Grid.SetColumn(QuestionsCriteria[i].slider, 1);

                Grid.SetRow(QuestionsCriteria[i].OptionA, i);
                Grid.SetRow(QuestionsCriteria[i].OptionB, i);
                Grid.SetRow(QuestionsCriteria[i].slider, i);


                scrollQuestionCriterias.Children.Add(QuestionsCriteria[i].OptionA);
                scrollQuestionCriterias.Children.Add(QuestionsCriteria[i].OptionB);
                scrollQuestionCriterias.Children.Add(QuestionsCriteria[i].slider);
            }
        }
        private void InitializeAlternativeQuestions()
        {
            scrollQuestionAlternatives.Children.Clear();
            QuestionsAlternative = new List<QuestionModel>();

            int k = 0;
            for (int i = 0; i < combinationAlternatives.CombinationList.Count; i++)
            {
                for (int j = 0; j < CriteriaQuestions.CriteriasNames.Count; j++)
                {
                    scrollQuestionAlternatives.RowDefinitions.Add(new RowDefinition());

                    string Category = CriteriaQuestions.CriteriasNames[j];
                    string A = AlternativeQuestions[combinationAlternatives.CombinationList[i].itemOne].parameters.parameters[j];
                    string B = AlternativeQuestions[combinationAlternatives.CombinationList[i].itemTwo].parameters.parameters[j];

                    QuestionsAlternative.Add(new QuestionModel(A, B,j));

                    Label categoryLabel = new Label();
                    categoryLabel.Content = Category;
                    categoryLabel.Height = 40;


                    Grid.SetColumn(categoryLabel, 0);
                    Grid.SetColumn(QuestionsAlternative[k].OptionA, 1);
                    Grid.SetColumn(QuestionsAlternative[k].OptionB, 3);
                    Grid.SetColumn(QuestionsAlternative[k].slider, 2);

                    Grid.SetRow(categoryLabel, k);
                    Grid.SetRow(QuestionsAlternative[k].OptionA, k);
                    Grid.SetRow(QuestionsAlternative[k].OptionB, k);
                    Grid.SetRow(QuestionsAlternative[k].slider, k);

                    scrollQuestionAlternatives.Children.Add(categoryLabel);
                    scrollQuestionAlternatives.Children.Add(QuestionsAlternative[k].OptionA);
                    scrollQuestionAlternatives.Children.Add(QuestionsAlternative[k].OptionB);
                    scrollQuestionAlternatives.Children.Add(QuestionsAlternative[k].slider);
                    k++;
                }
            }

        }

        private void Reset()
        {
            alternativesSomplete = criteriasComplete = false;
            Alternatives.Clear();
            ParametersList.Clear();
            criterias.Clear();
            InitializeListBoxes();
            InitializeTreeView();
        }

        private void EditCriteriaButton(object sender, RoutedEventArgs e)
        {
            int id = CriteriasListBox.SelectedIndex;
            if (id == -1)
            {
                editCriteriaWindow = new EditCriteriaWindow();
                if (editCriteriaWindow.ShowDialog() == true)
                {   
                    criterias.AddCriteria(editCriteriaWindow.GetCriteriaName());
                }
                for (int i = 0; i < Alternatives.Count; i++)
                {
                    Alternatives[i].AddParameter();
                }
            }
            else
            {
                editCriteriaWindow = new EditCriteriaWindow(criterias.CriteriasNames[id]);
                if (editCriteriaWindow.ShowDialog() == true)
                {
                    criterias.CriteriasNames[id] = editCriteriaWindow.GetCriteriaName();
                }
            }
            InitializeListBoxes();
            InitializeTreeView();
        }
        private void DeleteCriteriaButton(object sender, RoutedEventArgs e)
        {
            int id = CriteriasListBox.SelectedIndex;
            if (id != -1)
            {
                criterias.RemoveCriteria(id);
                foreach (var item in Alternatives)
                {
                    item.RemoveParameter(id);
                }
                InitializeListBoxes();
                InitializeTreeView();
            }

        }
        private void EditAlternativeButton(object sender, RoutedEventArgs e)
        {
            int id = AlternativesListBox.SelectedIndex;
            if (id == -1)
            {
                editAlternativeWindow = new EditAlternativeWindow(criterias);
                if (editAlternativeWindow.ShowDialog() == true)
                {
                    Alternatives.Add(editAlternativeWindow.getResult());
                }
            }
            else
            {
                editAlternativeWindow = new EditAlternativeWindow(Alternatives[id],criterias);
                if (editAlternativeWindow.ShowDialog() == true)
                {
                    Alternatives[id] = editAlternativeWindow.getResult();
                }
            }
            InitializeListBoxes();
            InitializeTreeView();
        }
        private void DeleteAlternativeButton(object sender, RoutedEventArgs e)
        {
            int id = AlternativesListBox.SelectedIndex;
            if (id != -1)
            {
                Alternatives.RemoveAt(id);
                InitializeListBoxes();
                InitializeTreeView();
            }
        }
        private void SaveDataButton(object sender, RoutedEventArgs e)
        {
            if (Alternatives.Count < 2)
            {
                MessageBox.Show("Wymagane są conajmniej dwie alternatywy!");
            }
            else
            {
                if (criterias.CriteriasNames.Count < 2)
                {
                    MessageBox.Show("Wymagane są conajmniej dwa kryteria!");
                }
                else
                {
                    bool temp=true;
                    for (int i = 0; i < criterias.CriteriasNames.Count-1; i++)
                    {
                        for (int j = i+1; j < criterias.CriteriasNames.Count; j++)
                        {
                            if (criterias.CriteriasNames[i] == criterias.CriteriasNames[j])
                                temp = false;
                        }
                    }
                    for (int i = 0; i < Alternatives.Count-1; i++)
                    {
                        for (int j = i+1; j < Alternatives.Count; j++)
                        {
                            if (Alternatives[i].Name == Alternatives[j].Name)
                                temp = false;
                        }
                    }
                    if (!temp)
                    {
                        MessageBox.Show("Kryteria i alternatywy muszą mieć unikalne nazwy!");
                    }
                    else
                    {
                        newFileWindow = new NewFileWindow(fileList.Data);
                        if (newFileWindow.ShowDialog() == true)
                        {
                            IOFile.SaveData(newFileWindow.GetFileName(), Alternatives, criterias);
                            fileList.AddFile(newFileWindow.GetFileName());
                            fileList.SaveData();
                            InitializeFileList();
                        }
                    }
                }
            }
        }
        private void ClearDataButton(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        private void LoadDataButton(object sender, RoutedEventArgs e)
        {
            int id = FileList.SelectedIndex;
            if (id != -1)
            {
                IOFile = new LoadSaveFile();
                IOFile.LoadData(fileList.Data[id]);

                Alternatives.Clear();
                criterias.Clear();
                Alternatives = IOFile.getAlternatives();
                criterias = IOFile.getCriterias();

                InitializeListBoxes();
                InitializeTreeView();
                InitializeFileList();
            }
        }
        private void DeleteFileButton(object sender, RoutedEventArgs e)
        {
            int id = FileList.SelectedIndex;
            if (id != -1)
            {
                fileList.DeleteFile(id);
                InitializeListBoxes();
                InitializeTreeView();
                InitializeFileList();
            }
        }
        private void SelectFileButton(object sender, RoutedEventArgs e)
        {
            selectFileWindow = new SelectFileWindow();
            if (selectFileWindow.ShowDialog() == true)
            {
                InitializeSelectBoxes(selectFileWindow.getID());
            }

        }
        

        private Criterias Sort(Criterias list)
        {
            index = new List<int>();
            int j = 0;
            for (int i = 0; i < criterias.CriteriasNames.Count; i++)
            {
                if (list.CriteriasNames.Contains(criterias.CriteriasNames[i]))
                {
                    index.Add(criterias.CriteriasNames.IndexOf(list.CriteriasNames[j]));
                    j++;
                    if (j == list.CriteriasNames.Count) { break; }
                }
            }
            do
            {
                for (int i = 0; i < index.Count-1; i++)
                {
                    if (index[i] > index[i + 1])
                    {
                        int t = index[i];
                        index[i] = index[i + 1];
                        index[i + 1] = t;

                        string s = list.CriteriasNames[i];
                        list.CriteriasNames[i] = list.CriteriasNames[i + 1];
                        list.CriteriasNames[i + 1] = s;
                    }
                }
                j--;
            } while (j > 1);
            return list;
        }
        private List<Alternative> SortAlternatives(List<Alternative> list)
        {
            List<int> id = new List<int>();
            int j = 0;
            for (int i = 0; i < list.Count; i++)
            {
               for(int k = 0; k < Alternatives.Count; k++)
                {
                    if (list[i].Name == Alternatives[k].Name)
                    {
                        id.Add(k);
                        j++;
                    }
                }
            }

            do
            {
                for (int i = 0; i < id.Count-1; i++)
                {
                    if (id[i] > id[i + 1])
                    {
                        int t = id[i];
                        id[i] = id[i + 1];
                        id[i + 1] = t;

                        Alternative s = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = s;
                    }
                }
                j--;
            } while (j > 1);

            return list;
        }

        private List<Alternative> finalAlternatives(List<string> list)
        {
            List<Alternative> newList = new List<Alternative>();

            for(int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < Alternatives.Count; j++)
                {
                    if (list[i] == Alternatives[j].Name)
                    {
                        Parameters newParameters = new Parameters();
                        for (int k = 0; k < index.Count; k++)
                        {
                            newParameters.AddOnceParameter(Alternatives[j].parameters.parameters[index[k]]);
                        }
                        newList.Add(new Alternative(Alternatives[j].Name, newParameters));
                    }
                }
            }
            newList = SortAlternatives(newList);
            return newList;
        }

        private void CreateQuestionsButton(object sender, RoutedEventArgs e)
        {
            
            Criterias tmp = new Criterias();
            var idCriteria = CriteriaSwitch.SelectedItems;
            if (idCriteria.Count < 2)
            {
                MessageBox.Show("Wybierz conajmniej dwa kryteria");
            }
            else
            {
                for (int i = 0; i < idCriteria.Count; i++)
                {
                    tmp.AddCriteria(idCriteria[i].ToString());
                }
                CriteriaQuestions = Sort(tmp);

                
                var idAlternative = AlternativeSwitch.SelectedItems;
                if (idAlternative.Count < 2)
                {
                    MessageBox.Show("Wybierz conajmniej dwie alternatywy");
                }
                else
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < idAlternative.Count; i++)
                    {
                        list.Add(idAlternative[i].ToString());
                    }
                    AlternativeQuestions = finalAlternatives(list);
                    alternativesSomplete = criteriasComplete = true;
                    combinationsCriteria.SetCouples(CriteriaQuestions.CriteriasNames.Count);
                    combinationAlternatives.SetCouples(AlternativeQuestions.Count);


                    InitializeCriteriaQuestions();
                    InitializeAlternativeQuestions();
                }
            }
        }

        private void FinishButton(object sender, RoutedEventArgs e)
        {
            if(alternativesSomplete && criteriasComplete)
            {
                weightsCriteria = new Weights(CriteriaQuestions.CriteriasNames.Count);

                for (int i = 0; i < QuestionsCriteria.Count; i++)
                {
                    double weight = QuestionsCriteria[i].getValue();
                    int x = combinationsCriteria.CombinationList[i].itemOne;
                    int y = combinationsCriteria.CombinationList[i].itemTwo;

                    weightsCriteria.InsertToTable(x, y, weight);
                }

                weightsAlternatives = new List<Weights>();
                int idc = 0;
                for (int i = 0; i < CriteriaQuestions.CriteriasNames.Count; i++)
                {
                    weightsAlternatives.Add(new Weights(AlternativeQuestions.Count));



                }
                for (int i = 0; i < combinationAlternatives.CombinationList.Count; i++)
                {
                    for (int j = 0; j < CriteriaQuestions.CriteriasNames.Count; j++)
                    {
                        int x = combinationAlternatives.CombinationList[i].itemOne;
                        int y = combinationAlternatives.CombinationList[i].itemTwo;
                        double weight = QuestionsAlternative[idc].getValue();
                        int id = QuestionsAlternative[idc].compare;
                        weightsAlternatives[id].InsertToTable(x, y, weight);
                        idc++;
                    }
                }

                cValues = new CriteriaValues(CriteriaQuestions.CriteriasNames.Count);
                cValues.Count(weightsCriteria.weights);

                aValues = new List<CriteriaValues>();
                for (int i = 0; i < CriteriaQuestions.CriteriasNames.Count; i++)
                {
                    aValues.Add(new CriteriaValues(AlternativeQuestions.Count));
                    aValues[i].Count(weightsAlternatives[i].weights);
                }


                Validate validateCriterias = new Validate(CriteriaQuestions.CriteriasNames.Count, cValues.Values, weightsCriteria.weights, errorLevel);
                List<Validate> validateAlternatives = new List<Validate>();
                for (int i = 0; i < CriteriaQuestions.CriteriasNames.Count; i++)
                {
                    validateAlternatives.Add(new Validate(AlternativeQuestions.Count, aValues[i].Values, weightsAlternatives[i].weights, errorLevel));
                }
                bool correctly = true;
                correctly = validateCriterias.Cohesion();
                foreach (Validate item in validateAlternatives)
                {
                    correctly = item.Cohesion();
                }
                if (!correctly)
                {
                    ValidationResult vr = new ValidationResult();
                    if (vr.ShowDialog() == true)
                    {
                        correctly = vr.confirm();
                    }
                }
                if (correctly)
                {
                    RatingAlternatives ratingAlternatives = new RatingAlternatives(AlternativeQuestions.Count, CriteriaQuestions.CriteriasNames.Count);
                    ratingAlternatives.Count(cValues.Values, aValues);
                    data = new List<KeyValuePair<string, double>>();
                    data.Clear();

                    for (int i = 0; i < AlternativeQuestions.Count; i++)
                    {
                        data.Add(new KeyValuePair<string, double>(AlternativeQuestions[i].Name, ratingAlternatives.Values[i]));
                    }
                    chartWindow = new ChartWindow(data);
                    chartWindow.Show();
                }

            }
        }
    }
}
