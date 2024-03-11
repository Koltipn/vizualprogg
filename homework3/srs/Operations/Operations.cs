using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Data;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia.Controls;
using System.Formats.Tar;


namespace Домашнее_задание__3;
public class Operations : INotifyPropertyChanged
{
    private string _numberInput = "";
    private double _firstNumber = 0;
    private double _secondNumber = 0;
    private string _operand = "";
    private double _result = 0;

    private double _lastNumber= 0;

    public string NumberInput
    {
        get => _numberInput;
        set => SetField(ref _numberInput, value);
    }

    public double lastNumber
    {
        get => _lastNumber;
        set => SetField(ref _lastNumber, value);
    }

    public double FirstNumber
    {
        get => _firstNumber;
        set => SetField(ref _firstNumber, value);
    }

    public double SecondNumber
    {
        get => _secondNumber;
        set => SetField(ref _secondNumber, value);
    }

    public string Operand
    {
        get => _operand;
        set => SetField(ref _operand, value);
    }

    public double Result
    {
        get => _result;
        set => SetField(ref _result, value);
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

    public void SetNumberInput(object parameter)
    {
        // Добавляем новый ввод к списку
        if (parameter is string number)
        {
            NumberInput += number;
            // Обновляем значение _firstNumber или _secondNumber
            if (Operand == "")
            {
                // Если нет операнда, обновляем _firstNumber
                FirstNumber = double.Parse(NumberInput);
            }
            else
            {
                // Если есть операнд, обновляем _secondNumber
                SecondNumber = double.Parse(NumberInput);
            }
        }
    }

    public void SetOperator(object parameter)
    {
        _operand = parameter.ToString();

        // Очищаем строку ввода
        NumberInput = "";
    }

    public void EqualsClick()
    {
        CalculateResult();
        _firstNumber = _result;
        NumberInput = "";
    }

public void CL()
{
    Result = 0.0;
    FirstNumber = 0.0;
    SecondNumber = 0.0;
    NumberInput = "";
    Operand = "";
}

public void CalculateResult()
{
    switch (_operand)
    {
        case "+":
            Result = (_firstNumber + _secondNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "-":
            Result = (FirstNumber - SecondNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "*":
            Result = (FirstNumber * SecondNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "/":
            Result = (FirstNumber / SecondNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "%":
            Result = (FirstNumber % SecondNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "n!":
            _result = 1;
            for (int i = 1; i <= _firstNumber; i++)
                _result *= i;
            OnPropertyChanged(nameof(Result));
            break;
        case "^": // Возведение в степень
            Result = Math.Pow(_firstNumber, _secondNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "cos": // Косинус
            Result = Math.Cos(_firstNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "sin": // Синус
            Result = Math.Sin(_firstNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "tan": // Тангенс
            Result = Math.Tan(_firstNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "lg": // Логарифм по основанию 10
            Result = Math.Log10(_firstNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "ln": // Натуральный логарифм
            Result = Math.Log(_firstNumber);
            OnPropertyChanged(nameof(Result));
            break;
        case "floor": // Округление до меньшего целого
            Result = Math.Floor(_result);
            OnPropertyChanged(nameof(Result));
            break;
        case "ceil": // Округление до большего целого
            Result = Math.Ceiling(_result);
            OnPropertyChanged(nameof(Result));
            break;
        default:
            break;
    }
}

}
