using System;
using System.Collections.Generic;

public interface INotifyer
{
    void Notify(decimal balance);
}


public class SMSLowBalanceNotifyer : INotifyer
{
    private string _phone;
    private decimal _LowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue)
    {                                                                  
        _phone = phone;
        _LowBalanceValue = lowBalanceValue;
    }

    public void Notify(decimal balance)
    {
        if (balance < _LowBalanceValue)
        {
            Console.WriteLine($"SMSLowBalanceNotifyer: Уведомление пользователя по телефону {_phone}. Обнаружен низкий баланс: {balance}");
        }
    }
}


public class EMailBalanceChangedNotifyer : INotifyer
{
    private string _email;

    public EMailBalanceChangedNotifyer(string email)
    {
        _email = email;
    }

    public void Notify(decimal balance)
    {
        Console.WriteLine($"EMailBalanceChangedNotifyer: Уведомление пользователя по электронной почте {_email}. Новый баланс {balance}");
    }
}

//аккаунта пользователя
public class Account
{
    private decimal _balance; 
    private List<INotifyer> _notifyers; 

    public Account()
    {
        _balance = 0;
        _notifyers = new List<INotifyer>();
    }

    public Account(decimal initialBalance)
    {
        _balance = initialBalance;
        _notifyers = new List<INotifyer>();
    }

    public void AddNotifyer(INotifyer notifyer)
    {
        _notifyers.Add(notifyer);
    }

    public void ChangeBalance(decimal value)
    {                                        
        _balance = value;
        Notification();
    }

    public decimal Balance
    {
        get {
            return _balance;
        }
    }

    private void Notification()
    {
        for(int i = 0; i < _notifyers.Count(); i++)
        {
            _notifyers[i].Notify(_balance);
        }                                   
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите порог низкого баланса: ");
        decimal lowbalance = Convert.ToDecimal(Console.ReadLine());

        Account userAccount = new Account(1000);

        SMSLowBalanceNotifyer smsNotifyer = new SMSLowBalanceNotifyer("89132754843", lowbalance);
        EMailBalanceChangedNotifyer emailNotifyer = new EMailBalanceChangedNotifyer("mr.droidcaxa@mail.ru");

        userAccount.AddNotifyer(smsNotifyer);
        userAccount.AddNotifyer(emailNotifyer);

        Console.Write("Введите сумму для изменения баланса: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        userAccount.ChangeBalance(amount);
    }
}