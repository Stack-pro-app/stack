package com.ProjectMana.ProjectManagementSpring.DTO;

public class userDTO {
    public Integer id ;

    public String userName ;
    public  String role ;

    public String email ;

    public userDTO(Integer id, String userName, String role, String email) {
        this.id = id;
        this.userName = userName;
        this.role = role;
        this.email = email;
    }

    public String getUserName() {
        return userName;
    }
}
