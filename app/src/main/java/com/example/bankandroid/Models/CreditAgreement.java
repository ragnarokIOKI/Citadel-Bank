package com.example.bankandroid.Models;

import java.util.Date;

public class CreditAgreement {
    public String idCreditAgreement;
    public String dateDrawing;
    public String dateTermination;
    public double creditRate;
    public int creditAgreementUserId;
    public String creditApplicationId;
    public boolean creditAgreementDeleted;

    public CreditAgreement(String idCreditAgreement, String dateDrawing, String dateTermination, double creditRate, int creditAgreementUserId, String creditApplicationId, boolean creditAgreementDeleted) {
        this.idCreditAgreement = idCreditAgreement;
        this.dateDrawing = dateDrawing;
        this.dateTermination = dateTermination;
        this.creditRate = creditRate;
        this.creditAgreementUserId = creditAgreementUserId;
        this.creditApplicationId = creditApplicationId;
        this.creditAgreementDeleted = creditAgreementDeleted;
    }

    public String getIdCreditAgreement() {
        return idCreditAgreement;
    }

    public void setIdCreditAgreement(String idCreditAgreement) {
        this.idCreditAgreement = idCreditAgreement;
    }

    public String getDateDrawing() {
        return dateDrawing;
    }

    public void setDateDrawing(String dateDrawing) {
        this.dateDrawing = dateDrawing;
    }

    public String getDateTermination() {
        return dateTermination;
    }

    public void setDateTermination(String dateTermination) {
        this.dateTermination = dateTermination;
    }

    public double getCreditRate() {
        return creditRate;
    }

    public void setCreditRate(double creditRate) {
        this.creditRate = creditRate;
    }

    public int getCreditAgreementUserId() {
        return creditAgreementUserId;
    }

    public void setCreditAgreementUserId(int creditAgreementUserId) {
        this.creditAgreementUserId = creditAgreementUserId;
    }

    public String getCreditApplicationId() {
        return creditApplicationId;
    }

    public void setCreditApplicationId(String creditApplicationId) {
        this.creditApplicationId = creditApplicationId;
    }

    public boolean isCreditAgreementDeleted() {
        return creditAgreementDeleted;
    }

    public void setCreditAgreementDeleted(boolean creditAgreementDeleted) {
        this.creditAgreementDeleted = creditAgreementDeleted;
    }
}
