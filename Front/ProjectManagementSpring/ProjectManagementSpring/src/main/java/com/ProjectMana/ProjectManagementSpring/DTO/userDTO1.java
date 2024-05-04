package com.ProjectMana.ProjectManagementSpring.DTO;

import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;


@Data
@NoArgsConstructor
public class userDTO1  {
    public Integer id ;

    public String userName ;
    public  String AuthId;

    public String email ;


    public  List<taskDTO1> l ;


    public userDTO1(Integer id, String userName, String AuthId, String email, List<taskDTO1> l) {
        this.id = id;
        this.userName = userName;
        this.AuthId = AuthId;
        this.email = email;
        this.l = l;
    }
}
