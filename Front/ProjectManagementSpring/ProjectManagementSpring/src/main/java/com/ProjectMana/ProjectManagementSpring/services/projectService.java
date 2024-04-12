package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.projectDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.projectRepo;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class projectService {
    private com.ProjectMana.ProjectManagementSpring.repo.projectRepo projectRepo ;

    public projectService(com.ProjectMana.ProjectManagementSpring.repo.projectRepo projectRepo) {
        this.projectRepo = projectRepo;
    }
    public List<projectDTO> getAll(){
        List<projectDTO> l = new ArrayList<>();
        List<project> l1 = this.projectRepo.findAll();
        for(project p : l1){
            l.add(new projectDTO(p.getId(),p.getProjectName(),p.getProjectDescrp(),p.getStart(),p.getEnd(),p.getBudget(),p.getClientName()));
        }
        return l ;
    }
    public projectDTO getById(int id){
        project p = this.projectRepo.findById(id).get();
       return new projectDTO(p.getId(),p.getProjectName(),p.getProjectDescrp(),p.getStart(),p.getEnd(),p.getBudget(),p.getClientName());



    }
    public projectDTO post(projectDTO prj ){
      project p = new project(prj.getId(),prj.getProjectName(),prj.getProjectDescrp(),prj.getStart(),prj.getEnd(),prj.getBudget(),prj.getClientName(),null);
      prj.setId(this.projectRepo.save(p).getId()) ;
      return prj ;
    }
    public Integer getIdByName(String name){
      project p = this.projectRepo.findByprojectName(name);
      if(p!=null){
        return p.getId();
      }
      return null ;
    }
}
