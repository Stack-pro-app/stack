package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO1;
import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.userDTO1;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class userService {
    private UserRepo userRepo ;

    public userService(UserRepo userRepo) {
        this.userRepo = userRepo;
    }
    public List<userDTO > getAll(){
        List<userDTO> l = new ArrayList<>();
        List<UserT> l1  = this.userRepo.findAll();
        for(UserT u : l1){
            l.add(new userDTO(u.id,u.userName,u.role,u.email));
        }
        return l;
    }


}
