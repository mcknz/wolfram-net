pipeline {
  agent any
  stages {
    stage('Clean workspace') {
      steps {
        cleanWs()
      }
    }
    stage('Checkout code') {
      steps {
        git credentialsId: 'mcknz-ssh', url: 'git@github.com:mcknz/wolfram-net.git'
      }
    }
    stage('Run tests') {
      steps {
        sh '/usr/local/share/dotnet/dotnet test WolframNet --logger trx --results-directory ${WORKSPACE}'
        mstest testResultsFile:"**/*.trx", keepLongStdio: true
      }
    }
  }
}
