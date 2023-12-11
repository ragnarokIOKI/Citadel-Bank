package com.example.bankandroid.Models;

public class AccountType {
    public int idAccountType;
    public String nameAccountType;
    public boolean accountTypeDeleted;

    public AccountType(int idAccountType, String nameAccountType, boolean accountTypeDeleted) {
        this.idAccountType = idAccountType;
        this.nameAccountType = nameAccountType;
        this.accountTypeDeleted = accountTypeDeleted;
    }

    public int getIdAccountType() {
        return idAccountType;
    }

    public void setIdAccountType(int idAccountType) {
        this.idAccountType = idAccountType;
    }

    public String getNameAccountType() {
        return nameAccountType;
    }

    public void setNameAccountType(String nameAccountType) {
        this.nameAccountType = nameAccountType;
    }

    public boolean isAccountTypeDeleted() {
        return accountTypeDeleted;
    }

    public void setAccountTypeDeleted(boolean accountTypeDeleted) {
        this.accountTypeDeleted = accountTypeDeleted;
    }
}
