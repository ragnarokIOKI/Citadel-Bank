package com.example.bankandroid.Models;

public class Role {
    public int idRole;
    public String nameRole;
    public boolean roleDeleted;

    public Role(int idRole, String nameRole, boolean roleDeleted) {
        this.idRole = idRole;
        this.nameRole = nameRole;
        this.roleDeleted = roleDeleted;
    }

    public int getIdRole() {
        return idRole;
    }

    public void setIdRole(int idRole) {
        this.idRole = idRole;
    }

    public String getNameRole() {
        return nameRole;
    }

    public void setNameRole(String nameRole) {
        this.nameRole = nameRole;
    }

    public boolean isRoleDeleted() {
        return roleDeleted;
    }

    public void setRoleDeleted(boolean roleDeleted) {
        this.roleDeleted = roleDeleted;
    }
}
