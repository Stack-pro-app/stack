package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import com.ProjectMana.ProjectManagementSpring.repo.WorkSpaceRepo;

import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import  com.ProjectMana.ProjectManagementSpring.repo.projectRepo;


@Service
public class userService {
    private UserRepo userRepo ;
    private WorkSpaceRepo workSpaceRepo ;
    private  projectRepo projectRepo ;

  public userService(UserRepo userRepo, WorkSpaceRepo workSpaceRepo, com.ProjectMana.ProjectManagementSpring.repo.projectRepo projectRepo) {
    this.userRepo = userRepo;
    this.workSpaceRepo = workSpaceRepo;
    this.projectRepo = projectRepo;
  }

  public List<userDTO > getAll(){
        List<userDTO> l = new ArrayList<>();
        List<UserT> l1  = this.userRepo.findAll();
        for(UserT u : l1){
            l.add(new userDTO(u.id,u.userName,u.authId,u.email));

        }
        return l;
    }
  public userDTO post(userDTO user ){
    UserT u = new UserT(user.id,user.userName,user.authId,user.email,null);

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
  public List<String> getUsersAdmin(Integer id ){
    Optional<UserT> optionalUser = this.userRepo.findById(id);

    UserT u;
    if (optionalUser.isPresent()) {
      u = optionalUser.get();
      List<WorkSpace> l  = this.workSpaceRepo.findAllByAdmin(u);
      List<String> l1 =new ArrayList<>();
      if(l!=null){
        if(!l.isEmpty()){
          for(WorkSpace wrk : l){
            l1.addAll(wrk.getUsers());

          }
          return l1  ;
        }else {
          return null ;
        }

      }else{
        return null ;
      }



    } else {
     return null ;
    }



  }
  public boolean sameWork(Integer pro_id,Integer user_id){
    WorkSpace wrk  = this.projectRepo.findById(pro_id).get().getWorkSpace();
    UserT u = this.userRepo.findById(user_id).get();
    if(wrk.getUsers().contains(u.authId)){
      return true ;
    }
    return false ;
  }



}
