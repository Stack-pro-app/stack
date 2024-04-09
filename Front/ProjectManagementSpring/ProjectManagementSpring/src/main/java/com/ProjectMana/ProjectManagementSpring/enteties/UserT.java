package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.OneToMany;
import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import java.util.List;

@Entity


@AllArgsConstructor
@NoArgsConstructor

public class UserT {
    @Id
    @GeneratedValue
    public Integer id ;

     public String userName ;
     public  String role ;
     public String email;

    @OneToMany(mappedBy = "user")
    @JsonManagedReference
     List<Task> tasks;

    public UserT(Integer id) {
        this.id = id;
    }

}
