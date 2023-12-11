package com.example.bankandroid.Models;

public class AccountApplicationHistory {
    public int idAccountApplicationHistory;
    public String accountApplicationId;
    public String dateTime;
    public String changeType;
    public String changedColumn;
    public String oldValue;
    public String newValue;

    public AccountApplicationHistory(int idAccountApplicationHistory, String accountApplicationId, String dateTime, String changeType, String changedColumn, String oldValue, String newValue) {
        this.idAccountApplicationHistory = idAccountApplicationHistory;
        this.accountApplicationId = accountApplicationId;
        this.dateTime = dateTime;
        this.changeType = changeType;
        this.changedColumn = changedColumn;
        this.oldValue = oldValue;
        this.newValue = newValue;
    }

    public int getIdAccountApplicationHistory() {
        return idAccountApplicationHistory;
    }

    public void setIdAccountApplicationHistory(int idAccountApplicationHistory) {
        this.idAccountApplicationHistory = idAccountApplicationHistory;
    }

    public String getAccountApplicationId() {
        return accountApplicationId;
    }

    public void setAccountApplicationId(String accountApplicationId) {
        this.accountApplicationId = accountApplicationId;
    }

    public String getDateTime() {
        return dateTime;
    }

    public void setDateTime(String dateTime) {
        this.dateTime = dateTime;
    }

    public String getChangeType() {
        return changeType;
    }

    public void setChangeType(String changeType) {
        this.changeType = changeType;
    }

    public String getChangedColumn() {
        return changedColumn;
    }

    public void setChangedColumn(String changedColumn) {
        this.changedColumn = changedColumn;
    }

    public String getOldValue() {
        return oldValue;
    }

    public void setOldValue(String oldValue) {
        this.oldValue = oldValue;
    }

    public String getNewValue() {
        return newValue;
    }

    public void setNewValue(String newValue) {
        this.newValue = newValue;
    }
}
