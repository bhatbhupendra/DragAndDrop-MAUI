﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DragAndDropInMAUI.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropInMAUI.MVVM.ViewModels
{
	public partial class DashboardViewModel: ObservableObject
	{
		public ObservableCollection<TaskModel> TaskList { get; set; } = new ObservableCollection<TaskModel>();
		private List<TaskModel> _allTaskList = new List<TaskModel>();

		private TaskModel _draggedItem;

		[ObservableProperty]
		private int _selectedOption;

		[ObservableProperty]
		private int _newTaskCount;
		[ObservableProperty]
		private int _inProgressCount;
		[ObservableProperty]
		private int _inReviewCount;
		[ObservableProperty]
		private int _doneCount;
		[ObservableProperty]
		private bool _isBusy;
		public DashboardViewModel()
		{
			_allTaskList.Add(new TaskModel() { TaskName="Task 1",TaskStatus=(int)TaskStatusOptions.NewTask,TaskId=1});
			_allTaskList.Add(new TaskModel() { TaskName="Task 2",TaskStatus=(int)TaskStatusOptions.NewTask,TaskId=2});
			_allTaskList.Add(new TaskModel() { TaskName="Task 3",TaskStatus=(int)TaskStatusOptions.NewTask,TaskId=3});

			_allTaskList.Add(new TaskModel() { TaskName="Task 3",TaskStatus=(int)TaskStatusOptions.InProgress, TaskId = 4});
			_allTaskList.Add(new TaskModel() { TaskName="Task 4",TaskStatus=(int)TaskStatusOptions.InProgress, TaskId = 5 });

			_allTaskList.Add(new TaskModel() { TaskName="Task 6",TaskStatus=(int)TaskStatusOptions.InReview, TaskId = 6 });
			_allTaskList.Add(new TaskModel() { TaskName="Task 7",TaskStatus=(int)TaskStatusOptions.InReview, TaskId = 7 });

			_allTaskList.Add(new TaskModel() { TaskName="Task 8",TaskStatus=(int)TaskStatusOptions.Done, TaskId = 8 });
			AddTaskList();
		}

		private void SetCount()
		{
			NewTaskCount = _allTaskList.Count(f => f.TaskStatus == (int)TaskStatusOptions.NewTask);
			InProgressCount = _allTaskList.Count(f => f.TaskStatus == (int)TaskStatusOptions.InProgress);
			InReviewCount = _allTaskList.Count(f => f.TaskStatus == (int)TaskStatusOptions.InReview);
			DoneCount = _allTaskList.Count(f => f.TaskStatus == (int)TaskStatusOptions.Done);
		}
		private void AddTaskList()
		{
			var filterTaskList = _allTaskList.Where(f=> f.TaskStatus == SelectedOption).ToList();
			TaskList.Clear();
			foreach(var task in filterTaskList)
			{
				TaskList.Add(task);
			}
			SetCount();
		}
		[RelayCommand]
		public void FilterTaskList(string optionStr)
		{
			int option = Convert.ToInt32(optionStr);
			SelectedOption = -1;
			SelectedOption = option;
			AddTaskList();
		}

		[RelayCommand]
		public void DragStarted(TaskModel task)
		{
			_draggedItem=task;
		}
		[RelayCommand]
		public async void TaskDroped(string optionStr)
		{
			int option = Convert.ToInt32(optionStr);
			if(SelectedOption==option)return;
			IsBusy = true;
			//delay
			await Task.Delay(300);
			if(_draggedItem != null)
			{
				var currentItem = _allTaskList.Where(f=> f.TaskId == _draggedItem.TaskId).FirstOrDefault();
				currentItem.TaskStatus = option;

				AddTaskList();
			}
			IsBusy = false;
		}
	}
}
