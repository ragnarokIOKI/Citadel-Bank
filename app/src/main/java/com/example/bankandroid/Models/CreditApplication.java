package com.example.bankandroid.Models;

public class CreditApplication {
    public String idCreditApplication;
    public double applicationAmount;
    public double creditDesiredPercentage;
    public int creditApplicationUserId;
    public int statusId;

    public CreditApplication(String idCreditApplication, double applicationAmount, double creditDesiredPercentage, int creditApplicationUserId, int statusId) {
        this.idCreditApplication = idCreditApplication;
        this.applicationAmount = applicationAmount;
        this.creditDesiredPercentage = creditDesiredPercentage;
        this.creditApplicationUserId = creditApplicationUserId;
        this.statusId = statusId;
    }

    public String getIdCreditApplication() {
        return idCreditApplication;
    }

    public void setIdCreditApplication(String idCreditApplication) {
        this.idCreditApplication = idCreditApplication;
    }

    public double getApplicationAmount() {
        return applicationAmount;
    }

    public void setApplicationAmount(double applicationAmount) {
        this.applicationAmount = applicationAmount;
    }

    public double getCreditDesiredPercentage() {
        return creditDesiredPercentage;
    }

    public void setCreditDesiredPercentage(double creditDesiredPercentage) {
        this.creditDesiredPercentage = creditDesiredPercentage;
    }

    public int getCreditApplicationUserId() {
        return creditApplicationUserId;
    }

    public void setCreditApplicationUserId(int creditApplicationUserId) {
        this.creditApplicationUserId = creditApplicationUserId;
    }

    public int getStatusId() {
        return statusId;
    }

    public void setStatusId(int statusId) {
        this.statusId = statusId;
    }
}
