package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.WorkSpaceDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.projectDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import com.ProjectMana.ProjectManagementSpring.repo.WorkSpaceRepo;

import com.ProjectMana.ProjectManagementSpring.repo.projectRepo;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class projectService {
    private com.ProjectMana.ProjectManagementSpring.repo.projectRepo projectRepo ;
    private WorkSpaceRepo workSpaceRepo ;
    private UserRepo userRepo ;

  public projectService(com.ProjectMana.ProjectManagementSpring.repo.projectRepo projectRepo, WorkSpaceRepo workSpaceRepo, UserRepo userRepo) {
    this.projectRepo = projectRepo;
    this.workSpaceRepo = workSpaceRepo;
    this.userRepo = userRepo;
  }

  public List<projectDTO> getAll(){
        List<projectDTO> l = new ArrayList<>();
        List<project> l1 = this.projectRepo.findAll();
        for(project p : l1){
            l.add(new projectDTO(p.getId(),p.getProjectName(),p.getProjectDescrp(),p.getStart(),p.getEnd(),p.getBudget(),p.getClientName(),p.getWorkSpace().getId(),p.getWorkSpace().getName()));

        }
        return l ;
    }
    public projectDTO getById(int id){
        project p = this.projectRepo.findById(id).get();
       return new projectDTO(p.getId(),p.getProjectName(),p.getProjectDescrp(),p.getStart(),p.getEnd(),p.getBudget(),p.getClientName(),p.getWorkSpace().getId(),p.getWorkSpace().getName());




    }
    public projectDTO post(projectDTO prj ){

      WorkSpace wrk  = this.workSpaceRepo.findById(prj.getWorkId()).get();
      if(wrk!=null){
        project p = new project(prj.getId(),prj.getProjectName(),prj.getProjectDescrp(),prj.getStart(),prj.getEnd(),prj.getBudget(),prj.getClientName(),wrk,null);
        prj.setId(this.projectRepo.save(p).getId()) ;
        return prj ;
      }
      return null ;



    }
    public Integer getIdByName(String name){
      project p = this.projectRepo.findByprojectName(name);
      if(p!=null){
        return p.getId();
      }
      return null ;
    }

  public List<projectDTO> getAdminProjects(Integer id ){
    UserT u = this.userRepo.findById(id) .get() ;
    if(u!=null){
      List<WorkSpace> l1 = this.workSpaceRepo.findAllByAdmin(u);
      List<projectDTO> l = new ArrayList<>();
      List<project> l2 = this.projectRepo.findAllByWorkSpaceIn(l1);
      for(project p : l2){
        l.add(new projectDTO(p.getId(),p.getProjectName(),p.getProjectDescrp(),p.getStart(),p.getEnd(),p.getBudget(),p.getClientName(),p.getWorkSpace().getId(),p.getWorkSpace().getName()));
      }
      return l ;
    }
    return null ;


  }
  public List<WorkSpaceDTO> getAdminWorkspaces(Integer id ){
    UserT u = this.userRepo.findById(id) .get() ;
    List<WorkSpace> wrks = this.workSpaceRepo.findAllByAdmin(u);
    List<WorkSpaceDTO> l = new ArrayList<>();
    if(wrks!=null){
      for(WorkSpace wrk : wrks){
        WorkSpaceDTO tmp = new WorkSpaceDTO(wrk.getId(),wrk.getName(),wrk.getAdmin().userName,wrk.getUsers());
        l.add(tmp);
      }
      return l ;
    }
    return null ;




  }
  public List<project> getAdminProject0(Integer id ){
    UserT u = this.userRepo.findById(id) .get() ;
    if(u!=null){
      List<WorkSpace> l1 = this.workSpaceRepo.findAllByAdmin(u);

      List<project> l2 = this.projectRepo.findAllByWorkSpaceIn(l1);
      return l2 ;



  }else {
      return null ;
    }}
}


