package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.WorkSpaceDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.projectDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.repo.WorkSpaceRepo;
import com.ProjectMana.ProjectManagementSpring.services.workService;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController

public class WorkSpaceController {
  private  WorkSpaceRepo  WorkSpaceRepo ;
  private workService  wrkService ;

  public WorkSpaceController(com.ProjectMana.ProjectManagementSpring.repo.WorkSpaceRepo workSpaceRepo, workService wrkService) {
    WorkSpaceRepo = workSpaceRepo;
    this.wrkService = wrkService;
  }

  @PostMapping(value = "/workspace")
  public WorkSpaceDTO create(@RequestBody WorkSpaceDTO wrk){
    return this.wrkService.postWork(wrk) ;

  }

}
