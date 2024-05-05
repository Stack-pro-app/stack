package com.ProjectMana.ProjectManagementSpring.DTO;

import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.projectRepo;
import lombok.Data;

import java.util.ArrayList;
import java.util.List;
@Data
public class projectDTO {

    private Integer id ;

    private String  projectName ;

    private String  projectDescrp;

    private String  start;
    private String  end;

    private Integer  budget;

    private String clientName ;
    private Integer workId ;
    private String workName ;

  public projectDTO(Integer id, String projectName, String projectDescrp, String start, String end, Integer budget, String clientName, Integer workId, String workName) {
    this.id = id;
    this.projectName = projectName;
    this.projectDescrp = projectDescrp;
    this.start = start;
    this.end = end;
    this.budget = budget;
    this.clientName = clientName;
    this.workId = workId;
    this.workName = workName;
  }
}
