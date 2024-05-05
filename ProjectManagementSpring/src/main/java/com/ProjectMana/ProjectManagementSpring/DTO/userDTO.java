package com.ProjectMana.ProjectManagementSpring.DTO;

public class userDTO {
    public Integer id ;

    public String userName ;
    public  String AuthId ;

    public String email ;

    public userDTO(Integer id, String userName, String role, String email) {
        this.id = id;
        this.userName = userName;
        this.AuthId = role;
        this.email = email;
    }

    public String getUserName() {
        return userName;
    }
}
