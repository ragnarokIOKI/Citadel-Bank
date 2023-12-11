package com.example.bankandroid.Models;

public class OverpaymentDTO {
    public double creditAmount;
    public double creditRate;
    public String dateDrawing;
    public String dateTermination;
    public double overpayment;

    public OverpaymentDTO() {
    }

    public OverpaymentDTO(double creditAmount, double creditRate, String dateDrawing, String dateTermination, double overpayment) {
        this.creditAmount = creditAmount;
        this.creditRate = creditRate;
        this.dateDrawing = dateDrawing;
        this.dateTermination = dateTermination;
        this.overpayment = overpayment;
    }

    public double getCreditAmount() {
        return creditAmount;
    }

    public void setCreditAmount(double creditAmount) {
        this.creditAmount = creditAmount;
    }

    public double getCreditRate() {
        return creditRate;
    }

    public void setCreditRate(double creditRate) {
        this.creditRate = creditRate;
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

    public double getOverpayment() {
        return overpayment;
    }

    public void setOverpayment(double overpayment) {
        this.overpayment = overpayment;
    }
}
