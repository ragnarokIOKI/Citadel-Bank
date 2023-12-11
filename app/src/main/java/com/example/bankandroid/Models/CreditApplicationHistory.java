package com.example.bankandroid.Models;

public class CreditApplicationHistory {
    public int idCreditApplicationHistory;
    public String creditApplicationId;
    public String dateTime;
    public String changeType;
    public String changedColumn;
    public String oldValue;
    public String newValue;

    public CreditApplicationHistory(int idCreditApplicationHistory, String creditApplicationId, String dateTime, String changeType, String changedColumn, String oldValue, String newValue) {
        this.idCreditApplicationHistory = idCreditApplicationHistory;
        this.creditApplicationId = creditApplicationId;
        this.dateTime = dateTime;
        this.changeType = changeType;
        this.changedColumn = changedColumn;
        this.oldValue = oldValue;
        this.newValue = newValue;
    }

    public int getIdCreditApplicationHistory() {
        return idCreditApplicationHistory;
    }

    public void setIdCreditApplicationHistory(int idCreditApplicationHistory) {
        this.idCreditApplicationHistory = idCreditApplicationHistory;
    }

    public String getCreditApplicationId() {
        return creditApplicationId;
    }

    public void setCreditApplicationId(String creditApplicationId) {
        this.creditApplicationId = creditApplicationId;
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
