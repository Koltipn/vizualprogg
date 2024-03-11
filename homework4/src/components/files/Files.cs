using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Input;
using Avalonia.Controls;

namespace lab4;

internal class Files : INotifyPropertyChanged
{
    private ObservableCollection<string> _collection;
    private ObservableCollection<string> _files;

    private ObservableCollection<string> _dirName;

    private string _dirpath;

    public ObservableCollection<string> Collection
    {
        get => _collection;
        set => _ = SetField(ref _collection, value);
    }

    public ObservableCollection<string> FilesCollection
    {
        get => _files;
        set => _ = SetField(ref _files, value);
    }

    public ObservableCollection<string> DirName
    {
        get => _dirName;
        set => _ = SetField(ref _dirName, value);
    }

    public string DirPath
    {
        get => _dirpath;
        set => SetField(ref _dirpath, value);
    }

    public Files()
    {
        Collection = new ObservableCollection<string>(
            new string[] { "" });
        FilesCollection = new ObservableCollection<string>(
            new string[] { "" });
        DirName = new ObservableCollection<string>(
            new string[] { "" });
        DirPath = "C:/Users/Kolti/Desktop/ucheba/vizualprogg/homework4/src/components/files";
        string[] files = Directory.GetFiles(DirPath);
        string[] directories = Directory.GetDirectories(DirPath);
    
        foreach (string dir in directories)
        {
            Collection.Add(dir);
            DirName.Add(Path.GetFileName(dir));
        }

        foreach (string file in files)
        {
            FilesCollection.Add(Path.GetFileName(file));
        }
    }

public void GoTo(string newDir)
{
    FilesCollection.Clear();
    string newPath = Path.Combine(DirPath, newDir);
    Directory.SetCurrentDirectory(newPath);
    DirPath = Directory.GetCurrentDirectory(); // Обновляем DirPath
    string[] directories = Directory.GetDirectories(DirPath);
    string[] newFiles = Directory.GetFiles(DirPath);

    DirName.Clear();
    Collection.Clear();
    

    DirectoryInfo currentDirInfo = new DirectoryInfo(DirPath);
    DirectoryInfo parentDirInfo = currentDirInfo.Parent;

    if (parentDirInfo != null)
    {
        string parentDirPath = parentDirInfo.FullName;
        Collection.Add(parentDirPath);
        DirName.Add(Path.GetFileName(parentDirPath));
    }

    foreach (string dir in directories)
    {
        DirName.Add(Path.GetFileName(dir));
        Collection.Add(dir);
    }

    foreach (string file in newFiles)
    {
        FilesCollection.Add(Path.GetFileName(file));
    }
}


public void GoToParentDirectory()
{
    FilesCollection.Clear();
    DirectoryInfo currentDirInfo = new DirectoryInfo(DirPath);
    DirectoryInfo parentDirInfo = currentDirInfo.Parent;

    if (parentDirInfo != null)
    {
        string parentDirPath = parentDirInfo.FullName;
        Directory.SetCurrentDirectory(parentDirPath);
        DirPath = Directory.GetCurrentDirectory();

        Collection.Clear();
        DirName.Clear();

        string[] directories = Directory.GetDirectories(DirPath);
        foreach (string dir in directories)
        {
            DirName.Add(Path.GetFileName(dir));
            Collection.Add(dir);
        }

        string[] newFiles = Directory.GetFiles(DirPath);
        foreach (string file in newFiles)
        {
            FilesCollection.Add(Path.GetFileName(file));
        }
    }
}

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

