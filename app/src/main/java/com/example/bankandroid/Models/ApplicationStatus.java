package com.example.bankandroid.Models;

public class ApplicationStatus {
    public int idApplicationStatus;
    public String nameStatus;

    public ApplicationStatus(int idApplicationStatus, String nameStatus) {
        this.idApplicationStatus = idApplicationStatus;
        this.nameStatus = nameStatus;
    }

    public int getIdApplicationStatus() {
        return idApplicationStatus;
    }

    public void setIdApplicationStatus(int idApplicationStatus) {
        this.idApplicationStatus = idApplicationStatus;
    }

    public String getNameStatus() {
        return nameStatus;
    }

    public void setNameStatus(String nameStatus) {
        this.nameStatus = nameStatus;
    }
}
