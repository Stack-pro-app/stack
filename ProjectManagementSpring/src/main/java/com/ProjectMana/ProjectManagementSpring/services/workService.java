package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.WorkSpaceDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import com.ProjectMana.ProjectManagementSpring.repo.WorkSpaceRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class workService {

  private final WorkSpaceRepo workSpaceRepo ;
  private final UserRepo userRepo;


  public WorkSpaceDTO postWork(WorkSpaceDTO wrk ){

    var admin = userRepo.findByAuthId(wrk.getAdmin());
    if (admin != null){
      var result = workSpaceRepo.save(WorkSpace.builder()
        .Name(wrk.getName())
        .id(0)
        .admin(admin)
          .users(wrk.getUsers())
        .build());
      return wrk ;
    }
    return null;
  }
}
