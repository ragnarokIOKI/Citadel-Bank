package com.example.bankandroid.Models;

public class CreditAgreementHistory {
    public int idCreditAgreementHistory;
    public String creditAgreementId;
    public String dteTime;
    public String changeType;
    public String changedColumn;
    public String oldValue;
    public String newValue;

    public CreditAgreementHistory(int idCreditAgreementHistory, String creditAgreementId, String dteTime, String changeType, String changedColumn, String oldValue, String newValue) {
        this.idCreditAgreementHistory = idCreditAgreementHistory;
        this.creditAgreementId = creditAgreementId;
        this.dteTime = dteTime;
        this.changeType = changeType;
        this.changedColumn = changedColumn;
        this.oldValue = oldValue;
        this.newValue = newValue;
    }

    public int getIdCreditAgreementHistory() {
        return idCreditAgreementHistory;
    }

    public void setIdCreditAgreementHistory(int idCreditAgreementHistory) {
        this.idCreditAgreementHistory = idCreditAgreementHistory;
    }

    public String getCreditAgreementId() {
        return creditAgreementId;
    }

    public void setCreditAgreementId(String creditAgreementId) {
        this.creditAgreementId = creditAgreementId;
    }

    public String getDteTime() {
        return dteTime;
    }

    public void setDteTime(String dteTime) {
        this.dteTime = dteTime;
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
