package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.projectDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO1;
import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.userDTO1;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
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
  public userDTO post(userDTO user ){
    UserT u = new UserT(null,user.userName,user.role,user.email,null);
    user.id= this.userRepo.save(u).id;
    return user ;
  }
  public Integer getIdByName(String name){
   UserT u = this.userRepo.findByUserName(name);
    if(u!=null){
      return u.id;
    }
    return null ;
  }


}
