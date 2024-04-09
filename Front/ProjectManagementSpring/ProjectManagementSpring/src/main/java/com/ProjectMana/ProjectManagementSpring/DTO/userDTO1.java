package com.ProjectMana.ProjectManagementSpring.DTO;

import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;


@Data
@NoArgsConstructor
public class userDTO1  {
    public Integer id ;

    public String userName ;
    public  String role ;

    public String email ;


    public  List<taskDTO1> l ;


    public userDTO1(Integer id, String userName, String role, String email, List<taskDTO1> l) {
        this.id = id;
        this.userName = userName;
        this.role = role;
        this.email = email;
        this.l = l;
    }
}
