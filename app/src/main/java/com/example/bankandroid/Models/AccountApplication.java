package com.example.bankandroid.Models;

public class AccountApplication {

    public String idAccountApplication;
    public int typeAccountId;
    public double accountDesiredPercentage;
    public int accountApplicationUserId;
    public int statusId;
    public boolean bank_Card_Needed;

    public AccountApplication(String idAccountApplication, int typeAccountId, double accountDesiredPercentage, int accountApplicationUserId, int statusId, boolean bank_Card_Needed) {
        this.idAccountApplication = idAccountApplication;
        this.typeAccountId = typeAccountId;
        this.accountDesiredPercentage = accountDesiredPercentage;
        this.accountApplicationUserId = accountApplicationUserId;
        this.statusId = statusId;
        this.bank_Card_Needed = bank_Card_Needed;
    }

    public String getIdAccountApplication() {
        return idAccountApplication;
    }

    public void setIdAccountApplication(String idAccountApplication) {
        this.idAccountApplication = idAccountApplication;
    }

    public int getTypeAccountId() {
        return typeAccountId;
    }

    public void setTypeAccountId(int typeAccountId) {
        this.typeAccountId = typeAccountId;
    }

    public double getAccountDesiredPercentage() {
        return accountDesiredPercentage;
    }

    public void setAccountDesiredPercentage(double accountDesiredPercentage) {
        this.accountDesiredPercentage = accountDesiredPercentage;
    }

    public int getAccountApplicationUserId() {
        return accountApplicationUserId;
    }

    public void setAccountApplicationUserId(int accountApplicationUserId) {
        this.accountApplicationUserId = accountApplicationUserId;
    }

    public int getStatusId() {
        return statusId;
    }

    public void setStatusId(int statusId) {
        this.statusId = statusId;
    }

    public boolean isBank_Card_Needed() {
        return bank_Card_Needed;
    }

    public void setBank_Card_Needed(boolean bank_Card_Needed) {
        this.bank_Card_Needed = bank_Card_Needed;
    }
}
